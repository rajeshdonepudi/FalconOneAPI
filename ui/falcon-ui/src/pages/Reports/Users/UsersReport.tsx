import { lazy } from "react";
import Paper from "@mui/material/Paper";
import { useGetUsersCreatedByYearQuery } from "@services/User/UserManagementService";
import { Button, Stack } from "@mui/material";
import {
  CartesianGrid,
  LineChart,
  XAxis,
  YAxis,
  Tooltip as CTooltip,
  Legend,
  Line,
  PieChart,
  Pie,
  ResponsiveContainer,
  BarChart,
  Bar,
  Cell,
} from "recharts";
import AppPage from "@/components/ui-components/AppPage";
import NavUtilities from "@/utilities/NavUtilities";
import ArrowBackOutlinedIcon from "@mui/icons-material/ArrowBackOutlined";
import { useNavigate } from "react-router-dom";
import AppLoader from "@/components/ui-components/AppLoader";
import Grid from "@mui/material/Grid2";

const UsersReport = () => {
  /****
   * Queries
   */
  const { data: usersCreated, isLoading } = useGetUsersCreatedByYearQuery(null);

  const COLORS = [
    "#008DDA",
    "#41C9E2",
    "#ACE2E1",
    "#F7EEDD",
    "#007F73",
    "#4CCD99",
    "#FFC700",
    "#FFF455",
    "#FF407D",
    "#FFCAD4",
    "#40679E",
    "#1B3C73",
    "#B1B2FF",
    "#AAC4FF",
    "#6096B4",
  ];

  const navigate = useNavigate();

  return (
    <>
      <>
        <Button
          onClick={() => navigate(NavUtilities.ToSecureArea("reports"))}
          startIcon={<ArrowBackOutlinedIcon />}
        >
          Back to Reports
        </Button>
        <Grid container spacing={0.8}>
          <Grid size={{ xs: 12, md: 12 }}>
            <Paper>
              {isLoading ? (
                <Stack alignItems={"center"} justifyContent={"center"}>
                  <AppLoader />
                </Stack>
              ) : (
                <div style={{ width: "100%", height: 350 }}>
                  <ResponsiveContainer width="100%" height="100%">
                    <BarChart
                      width={500}
                      height={600}
                      data={usersCreated?.data as any}
                      margin={{
                        top: 5,
                        right: 10,
                        left: 10,
                        bottom: 5,
                      }}
                    >
                      <CartesianGrid strokeDasharray="3 3" />
                      <XAxis name="Year" dataKey="year" />
                      <YAxis
                        label={{
                          value: "Total Users Created",
                          angle: -90,
                          position: "insideBottomLeft",
                        }}
                      />
                      <CTooltip />
                      <Legend />
                      <Bar name="Year" dataKey="totalUsers" fill="#8884d8">
                        {COLORS.map((entry, index) => (
                          <Cell
                            key={`cell-${index}`}
                            fill={COLORS[index % COLORS.length]}
                          />
                        ))}
                      </Bar>
                    </BarChart>
                  </ResponsiveContainer>
                </div>
              )}
            </Paper>
          </Grid>
          <Grid size={6}>
            <Paper>
              {isLoading ? (
                <Stack alignItems={"center"} justifyContent={"center"}>
                  <AppLoader />
                </Stack>
              ) : (
                <div style={{ width: "100%", height: 350 }}>
                  <ResponsiveContainer width="100%" height="100%">
                    <LineChart
                      margin={{
                        top: 5,
                        right: 10,
                        left: 10,
                        bottom: 5,
                      }}
                      data={usersCreated?.data as any}
                    >
                      <CartesianGrid strokeDasharray="3 3" />
                      <XAxis
                        name="Year"
                        dataKey="year"
                        // padding={{ left: 30, right: 30 }}
                      />
                      <YAxis
                        label={{
                          value: "Total Users Created",
                          angle: -90,
                          position: "insideBottomLeft",
                        }}
                      />
                      <CTooltip />
                      <Legend />
                      <Line
                        name="Year"
                        type="monotone"
                        dataKey="totalUsers"
                        stroke={"#00ADB5"}
                        activeDot={{ r: 8 }}
                      />
                    </LineChart>
                  </ResponsiveContainer>
                </div>
              )}
            </Paper>
          </Grid>
          <Grid size={6}>
            <Paper>
              {isLoading ? (
                <Stack alignItems={"center"} justifyContent={"center"}>
                  <AppLoader />
                </Stack>
              ) : (
                <div style={{ width: "100%", height: 350 }}>
                  <ResponsiveContainer width="100%" height="100%">
                    <PieChart
                      margin={{
                        top: 5,
                        right: 10,
                        left: 10,
                        bottom: 5,
                      }}
                      width={400}
                      height={400}
                    >
                      <Pie
                        nameKey="year"
                        dataKey="totalUsers"
                        isAnimationActive={false}
                        data={usersCreated?.data as any}
                        cx="50%"
                        cy="50%"
                        outerRadius={80}
                        fill="#008DDA"
                        label
                      >
                        {COLORS.map((entry, index) => (
                          <Cell
                            key={`cell-${index}`}
                            fill={COLORS[index % COLORS.length]}
                          />
                        ))}
                      </Pie>
                      <CTooltip />
                    </PieChart>
                  </ResponsiveContainer>
                </div>
              )}
            </Paper>
          </Grid>
        </Grid>
      </>
    </>
  );
};

export default UsersReport;
