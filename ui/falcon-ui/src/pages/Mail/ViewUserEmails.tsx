import AddIcon from "@mui/icons-material/Add";
import { Button, Card, CardContent, Tab, Tabs } from "@mui/material";
import React, { useEffect, useState } from "react";
import AppTabPanel from "@/components/ui-components/AppTabPanel";
import ViewReceivedMails from "./ViewReceivedMails";
import ViewSentMails from "./ViewSentMails";
import { useNavigate, useSearchParams } from "react-router-dom";
import NavUtilities from "@/utilities/NavUtilities";
import AppPage from "@/components/ui-components/AppPage";
import AppPaper from "@/components/ui-components/AppPaper";

const ViewUserEmails = () => {
  const [value, setValue] = useState(0);
  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };
  const navigate = useNavigate();
  let [searchParams] = useSearchParams();

  useEffect(() => {
    if (searchParams.get("from") && Number(searchParams.get("from")) > -1) {
      setValue(Number(searchParams.get("from")));
    }
  }, [searchParams]);

  return (
    <AppPage
      title="Mails"
      rightHeaderActions={
        <Button
          style={{
            marginBottom: "1rem",
          }}
          onClick={() => navigate(NavUtilities.ToSecureArea("mails/compose"))}
          variant="contained"
          startIcon={<AddIcon />}
        >
          Compose Mail
        </Button>
      }
      content={
        <AppPaper>
          <CardContent>
            <Tabs
              value={value}
              onChange={handleChange}
              variant="scrollable"
              scrollButtons="auto"
              aria-label="scrollable auto tabs example"
            >
              <Tab label="Inbox" />
              <Tab label="Sent" />
            </Tabs>
            <AppTabPanel value={value} index={0}>
              <ViewReceivedMails />
            </AppTabPanel>
            <AppTabPanel value={value} index={1}>
              <ViewSentMails />
            </AppTabPanel>
          </CardContent>
        </AppPaper>
      }
    />
  );
};

export default ViewUserEmails;
