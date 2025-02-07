import VideoCall from "@/components/features/Video/VideoCall";
import AppLazyLoader from "@/components/ui-components/AppLazyLoader";
import { useRef } from "react";

const MeetingRoom = () => {
  return (
    <AppLazyLoader>
      <VideoCall />
    </AppLazyLoader>
  );
};

export default MeetingRoom;
