import MailViewer from "@/components/features/Mails/MailViewer";
import { useGetUserReceivedMailsQuery } from "@/services/Mail/MailService";

const ViewReceivedMails = () => {
  return (
    <MailViewer
      fetchMails={useGetUserReceivedMailsQuery}
      initialEndMessage="No new emails."
      fromView="0"
    />
  );
};

export default ViewReceivedMails;
