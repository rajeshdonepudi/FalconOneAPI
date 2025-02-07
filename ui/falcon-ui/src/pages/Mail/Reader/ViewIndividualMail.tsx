import {
  useLazyDownloadAttachmentQuery,
  useReadEmailMessageMutation,
} from "@/services/Mail/MailReaderService";
import { ReactNode, useEffect, useMemo } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import parse from "html-react-parser";
import { removeClassElementsParserOptions } from "@utilities/HTMLParser";
import AppPage from "@/components/ui-components/AppPage";
import {
  Button,
  Grid,
  IconButton,
  Paper,
  Skeleton,
  Stack,
  Typography,
} from "@mui/material";
import ArrowBackOutlinedIcon from "@mui/icons-material/ArrowBackOutlined";
import NavUtilities from "@/utilities/NavUtilities";
import AttachmentIcon from "@mui/icons-material/Attachment";
import DownloadIcon from "@mui/icons-material/Download";
import AppConstants from "@/constants/constants";

const ViewIndividualMail = () => {
  let [searchParams, setSearchParams] = useSearchParams();
  const navigate = useNavigate();

  const uid = useMemo(() => Number(searchParams.get("uid")), [searchParams]);
  const [readEmailMessage, { data, isLoading }] = useReadEmailMessageMutation();
  const [downloadAttachment] = useLazyDownloadAttachmentQuery();

  useEffect(() => {
    if (uid) {
      readEmailMessage({
        email: "bheeshma.hasthinapuram@gmail.com",
        password: "",
        mailBox: "INBOX",
        messageId: uid,
      });
    }
  }, [readEmailMessage, uid]);

  const download = (attachmentIndex: number, name: string) => {
    const attachmentId = `${uid}-${attachmentIndex}`;
    downloadAttachment({ attachmentId, name })
      .unwrap()
      .then((response) => {
        // Create a link element and trigger download
        const url = window.URL.createObjectURL(response);
        const link = document.createElement("a");
        link.href = url;
        link.download = name; // Replace with actual filename if available
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        window.URL.revokeObjectURL(url);
      });
  };

  const content = (): ReactNode => {
    if (isLoading) {
      return <Skeleton height={"400px"} />;
    }
    return (
      <Grid container spacing={AppConstants.layout.StandardSpacing}>
        <Grid item md={12}>
          <Paper variant="outlined" sx={{ padding: "1rem" }}>
            <Stack>
              <Stack direction={"row"}>
                <Typography variant="subtitle2">Attachments</Typography>
                <AttachmentIcon />
              </Stack>
              {data?.data.attachments.map((x, index) => (
                <Stack direction={"row"} alignItems={"center"}>
                  <Typography variant="body2">{x}</Typography>
                  <IconButton
                    onClick={() => download(index, x)}
                    aria-label="delete"
                  >
                    <DownloadIcon />
                  </IconButton>
                </Stack>
              ))}
            </Stack>
          </Paper>
        </Grid>
        <Grid item md={12}>
          {parse(data?.data?.body ?? "", removeClassElementsParserOptions)}
        </Grid>
      </Grid>
    );
  };

  return (
    <AppPage
      content={content()}
      title={data?.data.subject ?? "(No Subject)"}
      rightHeaderActions={
        <Button
          onClick={() => navigate(NavUtilities.ToSecureArea("messages/read"))}
          startIcon={<ArrowBackOutlinedIcon />}
        >
          Back to mails
        </Button>
      }
    />
  );
};

export default ViewIndividualMail;
