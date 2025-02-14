import React, { useState, useRef, useEffect, useMemo } from "react";
import * as signalR from "@microsoft/signalr";
import { toast } from "react-toastify";
import {
  Box,
  Button,
  Divider,
  Drawer,
  Grid,
  IconButton,
  List,
  ListItem,
  ListItemText,
  Stack,
  Typography,
  Tooltip,
  Paper,
} from "@mui/material";
import {
  Mic,
  MicOff,
  Videocam,
  VideocamOff,
  CallEnd,
  Group,
  Padding,
} from "@mui/icons-material";
import EnvUtilities from "@/utilities/EnvUtilities";
import CircleIcon from "@mui/icons-material/Circle";
import CallOutlinedIcon from "@mui/icons-material/CallOutlined";

import AppConstants from "@/constants/constants";
import { useAppDispatch, useAppSelector } from "@/hooks/StoreHooks";
import {
  openRightDrawer,
  closeRightDrawer,
  setRightDrawerContent,
} from "@/store/Slices/commonSlice";
import { useNavigate, useSearchParams } from "react-router-dom";
import NavUtilities from "@/utilities/NavUtilities";
import AppLazyLoader from "@/components/ui-components/AppLazyLoader";
import StringUtilities from "@/utilities/StringUtilities";
import SocketUtilities from "@/utilities/SocketUtilities";
import { CallerInfoDto } from "@/models/Users/UserLookupModel";

const VideoCall: React.FC = () => {
  const [localStream, setLocalStream] = useState<MediaStream | null>(null);
  const [connections, setConnections] = useState<CallerInfoDto[]>([]);
  const [participantsDrawer, setParticipantsDrawer] = useState(false);
  const [isAudioEnabled, setIsAudioEnabled] = useState(true);
  const [isVideoEnabled, setIsVideoEnabled] = useState(true);
  const [isCallStarted, setIsCallStarted] = useState(false);

  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();

  const meetName = useMemo(
    () => searchParams.get("name") ?? "",
    [searchParams]
  );

  const newConnection = useRef(
    SocketUtilities.getConnection(
      `${EnvUtilities.GetApiRootURL("hubs/meeting/pro")}`
    )
  );

  const peerConnection = useRef(
    new RTCPeerConnection({
      iceServers: [{ urls: "stun:stun.l.google.com:19302" }],
    })
  );

  const localVideoRef = useRef<HTMLVideoElement>(null);
  const remoteVideoRef = useRef<HTMLVideoElement>(null);

  const call = async (callerId: string) => {
    if (!peerConnection.current || !localStream) return;

    const offer = await peerConnection.current.createOffer({
      offerToReceiveAudio: true,
      offerToReceiveVideo: true,
    });

    await peerConnection.current.setLocalDescription(offer);

    try {
      await newConnection.current?.send(
        "sendOffer",
        newConnection.current.connectionId,
        callerId,
        JSON.stringify(offer)
      );
    } catch (error) {
      console.error(error);
    }
  };
  // eslint-disable-next-line react-hooks/exhaustive-deps
  const handleReceiveOffer = async (offerString: string, callerId: string) => {
    const offer = JSON.parse(offerString);
    dispatch(
      setRightDrawerContent(
        <Stack gap={2}>
          <Button
            variant="contained"
            color="primary"
            onClick={async () => {
              if (!peerConnection.current) {
                toast.error("Peer connection unavailable.");
                return;
              }

              await peerConnection.current.setRemoteDescription(
                new RTCSessionDescription(offer)
              );
              const answer = await peerConnection.current.createAnswer();
              await peerConnection.current.setLocalDescription(answer);

              await newConnection.current?.send(
                "sendAnswer",
                callerId,
                JSON.stringify(answer)
              );

              setIsCallStarted(true);
            }}
          >
            Answer
          </Button>
          <Button
            variant="outlined"
            color="error"
            onClick={() => dispatch(closeRightDrawer())}
          >
            Decline
          </Button>
        </Stack>
      )
    );
    dispatch(openRightDrawer());
  };

  // eslint-disable-next-line react-hooks/exhaustive-deps
  const handleReceiveAnswer = async (
    remoteGuy: string,
    answerString: string
  ) => {
    const answer = JSON.parse(answerString);

    if (peerConnection.current) {
      await peerConnection.current.setRemoteDescription(
        new RTCSessionDescription(answer)
      );
    }
  };

  // eslint-disable-next-line react-hooks/exhaustive-deps
  const handleReceiveIceCandidate = async (candidateString: string) => {
    const candidate = JSON.parse(candidateString);
    const newCandidate = new RTCIceCandidate(candidate);

    if (peerConnection.current) {
      try {
        await peerConnection.current.addIceCandidate(newCandidate);
      } catch (error) {
        console.error(error);
      }
    }
  };

  // eslint-disable-next-line react-hooks/exhaustive-deps
  const onIceCandidate = async (event: RTCPeerConnectionIceEvent) => {
    if (event.candidate) {
      const candidate = event.candidate.toJSON();
      try {
        await newConnection.current?.send(
          "sendIceCandidate",
          JSON.stringify(candidate)
        );
      } catch {}
    }
  };

  const gotRemoteStream = (event: RTCTrackEvent) => {
    if (
      remoteVideoRef.current &&
      remoteVideoRef.current.srcObject !== event.streams[0]
    ) {
      remoteVideoRef.current.srcObject = event.streams[0];
    }
  };

  useEffect(() => {
    if (newConnection.current) {
      newConnection.current.on("receiveOffer", handleReceiveOffer);
      newConnection.current.on("receiveAnswer", handleReceiveAnswer);
      newConnection.current.on(
        "receiveIceCandidate",
        handleReceiveIceCandidate
      );
      newConnection.current.on("UpdateConnections", setConnections);
    }
  }, [handleReceiveAnswer, handleReceiveIceCandidate, handleReceiveOffer]);

  useEffect(() => {
    if (peerConnection.current) {
      peerConnection.current.addEventListener("icecandidate", onIceCandidate);
      peerConnection.current.addEventListener("track", gotRemoteStream);
    }
  }, [onIceCandidate]);

  useEffect(() => {
    if (newConnection.current) {
      newConnection.current
        .start()
        .then(() => {
          navigator.mediaDevices
            .getUserMedia({ audio: true, video: true })
            .then((stream) => {
              setLocalStream(stream);
              if (localVideoRef.current) {
                localVideoRef.current.srcObject = stream;
              }

              stream
                .getTracks()
                .forEach((track) =>
                  peerConnection.current.addTrack(track, stream)
                );
            });
        })
        .catch((reason) => console.error(reason));
    }
  }, []);

  const toggleAudio = () => {
    if (localStream) {
      const audioTrack = localStream.getAudioTracks()[0];
      audioTrack.enabled = !audioTrack.enabled;
      setIsAudioEnabled(audioTrack.enabled);
    }
  };

  const toggleVideo = () => {
    if (localStream) {
      const videoTrack = localStream.getVideoTracks()[0];
      videoTrack.enabled = !videoTrack.enabled;
      setIsVideoEnabled(videoTrack.enabled);
    }
  };

  const hangup = () => {
    peerConnection.current?.close();
    localStream?.getTracks().forEach((track) => track.stop());

    toast.error("Call ended.");

    setTimeout(() => {
      navigate(NavUtilities.ToSecureArea("meetings"));
    }, 2000);
  };

  return (
    <AppLazyLoader>
      <Drawer
        variant="temporary"
        anchor="right"
        open={participantsDrawer}
        onClose={() => setParticipantsDrawer(false)}
      >
        <Box sx={{ width: "300px", padding: 2, marginTop: "3.5rem" }}>
          <Typography variant="h6" gutterBottom>
            Active Users ({connections.length})
          </Typography>
          <Divider />
          {connections.length > 0 ? (
            <List dense>
              {connections
                .filter(
                  (x) => x.connectionId !== newConnection.current.connectionId
                )
                .map((s, index) => (
                  <React.Fragment key={s.connectionId}>
                    <Stack direction={"row"} alignItems={"center"}>
                      <Stack
                        direction={"row"}
                        gap={AppConstants.layout.StandardSpacing}
                      >
                        <CircleIcon fontSize="small" color="success" />
                        <Typography>
                          {StringUtilities.addEllipsis(s.fullName, 15)}
                        </Typography>
                      </Stack>
                      <IconButton onClick={() => call(s.connectionId)}>
                        <CallOutlinedIcon />
                      </IconButton>
                    </Stack>
                    {index !== connections.length - 1 && (
                      <Divider component="li" />
                    )}
                  </React.Fragment>
                ))}
            </List>
          ) : (
            <Typography>No active users</Typography>
          )}
        </Box>
      </Drawer>
      <Grid container>
        <Grid item md={12}>
          <Paper variant="outlined" sx={{ padding: "0.5rem" }}>
            <Typography variant="h6">{meetName}</Typography>
          </Paper>
        </Grid>
      </Grid>
      <Grid
        container
        sx={{ height: "100vh", backgroundColor: "#f4f5f7", overflow: "hidden" }}
      >
        <Grid item xs={12} md={9} sx={{ height: "100%" }}>
          <video
            ref={remoteVideoRef}
            autoPlay
            playsInline
            muted
            style={{ height: "100%", width: "100%", objectFit: "cover" }}
          />
        </Grid>
        <Grid
          item
          xs={12}
          md={3}
          sx={{
            height: "100%",
            display: "flex",
            flexDirection: "column",
            justifyContent: "center",
            alignItems: "center",
            backgroundColor: "#ffffff",
            borderLeft: "1px solid #e0e0e0",
          }}
        >
          <Typography>You</Typography>

          <video
            ref={localVideoRef}
            autoPlay
            playsInline
            muted
            style={{
              height: "30%",
              width: "80%",
              borderRadius: "8px",
              objectFit: "cover",
              boxShadow: "0 4px 6px rgba(0, 0, 0, 0.1)",
            }}
          />
          <Stack direction="row" spacing={2} sx={{ marginTop: 2 }}>
            <Tooltip title={isAudioEnabled ? "Mute" : "Unmute"}>
              <IconButton
                color={isAudioEnabled ? "primary" : "error"}
                onClick={toggleAudio}
              >
                {isAudioEnabled ? <Mic /> : <MicOff />}
              </IconButton>
            </Tooltip>
            <Tooltip title={isVideoEnabled ? "Stop Video" : "Start Video"}>
              <IconButton
                color={isVideoEnabled ? "primary" : "error"}
                onClick={toggleVideo}
              >
                {isVideoEnabled ? <Videocam /> : <VideocamOff />}
              </IconButton>
            </Tooltip>
            <Tooltip title="End Call">
              <IconButton color="error" onClick={hangup}>
                <CallEnd fontSize="large" />
              </IconButton>
            </Tooltip>
            <Tooltip title="View Participants">
              <IconButton
                color="primary"
                onClick={() => setParticipantsDrawer(true)}
              >
                <Group />
              </IconButton>
            </Tooltip>
          </Stack>
        </Grid>
      </Grid>
    </AppLazyLoader>
  );
};

export default VideoCall;
