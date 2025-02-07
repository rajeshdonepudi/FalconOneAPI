import { useLazyGetMailInfoQuery } from "@/services/Mail/MailService";
import {
  Avatar,
  Button,
  Card,
  CardContent,
  Chip,
  Paper,
  Stack,
  Typography,
} from "@mui/material";
import { useEffect, useMemo } from "react";
import { useNavigate, useParams, useSearchParams } from "react-router-dom";
import ArrowBackOutlinedIcon from "@mui/icons-material/ArrowBackOutlined";
import NavUtilities from "@/utilities/NavUtilities";
import AppLazyLoader from "@/components/ui-components/AppLazyLoader";
import AppAccordion from "@/components/ui-components/AppAccordion";
import AppConstants from "@/constants/constants";
const ViewMail = () => {
  const { id } = useParams();
  const [getMailInfo, { data: response, isFetching }] =
    useLazyGetMailInfoQuery();
  let [searchParams, setSearchParams] = useSearchParams();
  const navigate = useNavigate();

  const mailInfo = useMemo(() => {
    return response?.data;
  }, [response?.data]);

  useEffect(() => {
    if (id) {
      getMailInfo(id);
    }
  }, [id]);

  const goBackToMails = () => {
    const from = Number(searchParams.get("from"));
    if (from > -1) {
      navigate(NavUtilities.ToSecureArea(`mails?from=${from}`));
    } else {
      navigate(NavUtilities.ToSecureArea("mails"));
    }
  };

  const getDefaultMessageLabel = () => {
    const from = Number(searchParams.get("from"));

    return from === 0 ? "Inbox" : "";
  };

  return (
    <AppLazyLoader>
      <Button onClick={goBackToMails} startIcon={<ArrowBackOutlinedIcon />}>
        Back to mails
      </Button>

      <Card variant="outlined">
        <CardContent>
          <Stack
            marginBottom={2}
            direction={"row"}
            alignItems={"center"}
            spacing={AppConstants.layout.StandardSpacing}
          >
            <Typography variant="h5">
              {mailInfo?.subject !== "" ? mailInfo?.subject : "(No subject)"}
            </Typography>
            <Stack>
              {getDefaultMessageLabel() && (
                <Chip
                  label={getDefaultMessageLabel()}
                  size="small"
                  variant="outlined"
                />
              )}
            </Stack>
          </Stack>
          <AppAccordion
            title="To"
            content={mailInfo?.toRecipients.map((r) => (
              <Chip
                avatar={
                  <Avatar
                    alt={r.fullName}
                    src={"data:image/gif;base64, " + r.avatar}
                  />
                }
                label={r.fullName}
                variant="outlined"
              />
            ))}
          />
          <AppAccordion
            title="Cc"
            content={mailInfo?.ccRecipients.map((r) => (
              <Chip
                avatar={
                  <Avatar
                    alt={r.fullName}
                    src={"data:image/gif;base64, " + r.avatar}
                  />
                }
                label={r.fullName}
                variant="outlined"
              />
            ))}
          />
          <AppAccordion
            title="Bcc"
            content={mailInfo?.bccRecipients.map((r) => (
              <Chip
                avatar={
                  <Avatar
                    alt={r.fullName}
                    src={"data:image/gif;base64, " + r.avatar}
                  />
                }
                label={r.fullName}
                variant="outlined"
              />
            ))}
          />

          <span
            dangerouslySetInnerHTML={{
              __html: mailInfo?.body ?? "",
            }}
          ></span>
        </CardContent>
      </Card>
    </AppLazyLoader>
  );
};

export default ViewMail;
