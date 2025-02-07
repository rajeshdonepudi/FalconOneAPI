import NewMail from "@/components/features/Mails/NewMail";
import AppLazyLoader from "@/components/ui-components/AppLazyLoader";
import { useSendNewMailMutation } from "@/services/Mail/MailService";
import NavUtilities from "@/utilities/NavUtilities";
import { Backdrop, Button, CircularProgress } from "@mui/material";
import { useSelector } from "react-redux";
import { useNavigate, useSearchParams } from "react-router-dom";
import ArrowBackOutlinedIcon from "@mui/icons-material/ArrowBackOutlined";

const ComposeMail = () => {
  const [sendNewMail, { isLoading }] = useSendNewMailMutation();
  var loggedInUser = useSelector((state: any) => state.auth);
  const navigate = useNavigate();
  let [searchParams, setSearchParams] = useSearchParams();

  const sendEmail = (values: any) => {
    sendNewMail({
      toRecipients: values.toRecipients.map((x: any) => x.id),
      ccRecipients: values.ccRecipients.map((x: any) => x.id),
      bccRecipients: values.bccRecipients.map((x: any) => x.id),
      subject: values.subject,
      body: values.body,
      senderId: loggedInUser.id,
    })
      .unwrap()
      .then(() => {
        navigate(NavUtilities.ToSecureArea("mails?from=1"));
      });
  };

  return (
    <AppLazyLoader>
      <Button
        onClick={() => navigate(-1)}
        startIcon={<ArrowBackOutlinedIcon />}
      >
        Back to mails
      </Button>
      <NewMail onMailComposed={sendEmail} />

      <Backdrop
        sx={{ color: "#fff", zIndex: (theme) => theme.zIndex.drawer + 1 }}
        open={isLoading}
      >
        <CircularProgress color="inherit" />
      </Backdrop>
    </AppLazyLoader>
  );
};

export default ComposeMail;
