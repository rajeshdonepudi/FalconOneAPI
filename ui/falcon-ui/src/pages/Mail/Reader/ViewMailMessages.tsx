import AppPage from "@/components/ui-components/AppPage";
import { useGetEmailMessagesMutation } from "@/services/Mail/MailReaderService";
import {
  Box,
  Button,
  Divider,
  ListItem,
  Skeleton,
  Stack,
  Typography,
  IconButton,
  Checkbox,
  List,
} from "@mui/material";
import { useCallback, useEffect, useState } from "react";
import RefreshIcon from "@mui/icons-material/Refresh";
import StarIcon from "@mui/icons-material/Star";
import MoreVertIcon from "@mui/icons-material/MoreVert";
import { useNavigate } from "react-router-dom";
import NavUtilities from "@/utilities/NavUtilities";
import parse, { Element } from "html-react-parser";
import { removeClassElementsParserOptions } from "@/utilities/HTMLParser";
import AppConstants from "@/constants/constants";

const ViewMailMessages = () => {
  const [getMailMessages, { data, isLoading }] = useGetEmailMessagesMutation();
  const [selectedMessages, setSelectedMessages] = useState<number[]>([]);
  const navigate = useNavigate();

  const refresh = useCallback(() => {
    getMailMessages({
      email: "bheeshma.hasthinapuram@gmail.com",
      password: "",
      mailBox: "INBOX",
    });
  }, [getMailMessages]);

  useEffect(() => {
    refresh();
  }, [refresh]);

  const handleSelectMessage = (messageId: number) => {
    setSelectedMessages((prevSelected) =>
      prevSelected.includes(messageId)
        ? prevSelected.filter((id) => id !== messageId)
        : [...prevSelected, messageId]
    );
  };

  const renderSkeleton = () => (
    <Stack spacing={AppConstants.layout.StandardSpacing}>
      {Array.from({ length: 5 }).map((_, index) => (
        <Box key={index}>
          <Skeleton variant="text" width="60%" />
          <Skeleton variant="text" width="80%" />
          {index !== 4 && <Divider />}
        </Box>
      ))}
    </Stack>
  );

  return (
    <AppPage
      title={"Inbox"}
      rightHeaderActions={
        <Button
          variant="outlined"
          startIcon={<RefreshIcon />}
          onClick={refresh}
        >
          Refresh
        </Button>
      }
      content={
        <Box>
          {isLoading ? (
            renderSkeleton()
          ) : (
            <List>
              {data?.data?.map((m, index) => (
                <ListItem
                  key={m.messageId}
                  sx={{
                    padding: "10px 16px",
                    backgroundColor: selectedMessages.includes(m.messageId)
                      ? "#e8f0fe"
                      : "transparent",
                    "&:hover": {
                      backgroundColor: "#f1f3f4",
                    },
                  }}
                >
                  <Checkbox
                    checked={selectedMessages.includes(m.messageId)}
                    onChange={() => handleSelectMessage(m.messageId)}
                    sx={{ marginRight: 1 }}
                  />
                  <Box
                    onClick={() =>
                      navigate(
                        NavUtilities.ToSecureArea(
                          `messages/read-mail?uid=${m.messageId}`
                        )
                      )
                    }
                    sx={{ flexGrow: 1 }}
                  >
                    <Stack
                      direction="row"
                      spacing={AppConstants.layout.StandardSpacing}
                      alignItems="center"
                    >
                      <IconButton>
                        <StarIcon />
                      </IconButton>
                      <Typography
                        variant="body2"
                        sx={{
                          fontWeight: m.isRead ? "normal" : "bold",
                          color: m.isRead ? "text.secondary" : "inherit",
                        }}
                      >
                        {m.from}
                      </Typography>
                      <Typography variant="body2" sx={{ fontWeight: "bold" }}>
                        {m.subject}
                      </Typography>
                      <Typography
                        variant="body2"
                        sx={{
                          color: "text.secondary",
                          flexGrow: 1,
                          overflow: "hidden",
                          textOverflow: "ellipsis",
                        }}
                      >
                        {m.snippet}
                      </Typography>
                    </Stack>
                  </Box>
                  <IconButton>
                    <MoreVertIcon />
                  </IconButton>
                </ListItem>
              ))}
            </List>
          )}
        </Box>
      }
    />
  );
};

export default ViewMailMessages;
