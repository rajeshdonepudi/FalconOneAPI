import MailViewer from "@/components/features/Mails/MailViewer";
import { useGetUserSentMailsQuery } from "@/services/Mail/MailService";

const ViewSentMails = () => {
  return (
    <MailViewer
      fetchMails={useGetUserSentMailsQuery}
      initialEndMessage="No more sent emails."
      fromView="1"
    />
  );
};

export default ViewSentMails;
