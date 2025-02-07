import AppLazyLoader from "@/components/ui-components/AppLazyLoader";
import { FilterLog } from "@/models/SystemLogs/FilterLog";
import { SystemLog } from "@/models/SystemLogs/SystemLog";
import {
  useGetControllerCodesQuery,
  useGetNoOfRequestsByResourceCodeQuery,
  useGetResourceCodesQuery,
  useLazyGetSystemLogsQuery,
} from "@/services/SystemLogs/LogsService";
import ColorUtilities from "@/utilities/ColorUtilities";
import {
  Box,
  Checkbox,
  CircularProgress,
  FormControl,
  Grid,
  InputLabel,
  ListItemText,
  MenuItem,
  OutlinedInput,
  Paper,
  Select,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import InfiniteScroll from "react-infinite-scroll-component";
import {
  Cell,
  Tooltip as CTooltip,
  ResponsiveContainer,
  BarChart,
  CartesianGrid,
  XAxis,
  YAxis,
  Legend,
  Bar,
} from "recharts";

const ViewLogs = () => {
  const [filterState, setFilterState] = useState<FilterLog>({
    page: 1,
    pageSize: 10,
    resourceCodes: [],
    controllerCodes: [],
  });
  const [logsInfo, setLogInfo] = useState<{
    hasMore: boolean;
    totalItems: number;
    logItems: SystemLog[];
  }>({
    hasMore: false,
    totalItems: 0,
    logItems: [],
  });
  const [getLogs, { data: logData }] = useLazyGetSystemLogsQuery();
  const { data: resourceCodes } = useGetResourceCodesQuery(null);
  const { data: controllerCodes } = useGetControllerCodesQuery(null);
  const { data: requestsByResourceCode } =
    useGetNoOfRequestsByResourceCodeQuery(null);

  const ITEM_HEIGHT = 80;
  const ITEM_PADDING_TOP = 8;
  const MenuProps = {
    PaperProps: {
      style: {
        maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
        width: 250,
      },
    },
  };

  useEffect(() => {
    getLogs(filterState, false);
  }, [filterState, getLogs]);

  useEffect(() => {
    setLogInfo((prev) => {
      return {
        ...prev,
        totalItems: logData?.data.totalItems ?? 0,
        logItems: [...prev.logItems, ...(logData?.data.items ?? [])],
        hasMore: logData?.data.isNextPage ?? false,
      };
    });
  }, [logData?.data]);

  useEffect(() => {
    getLogs(filterState, false);
  }, [filterState.page]);

  useEffect(() => {
    Promise.resolve(() => {
      setLogInfo((prev) => {
        return {
          ...prev,
          logItems: [],
          hasMore: false,
        };
      });
    }).then(() => {
      getLogs(filterState, false);
    });
  }, [filterState.resourceCodes, filterState.controllerCodes]);

  return (
    <AppLazyLoader>
      <Grid container spacing={0.8}>
        <Grid item md={12} xs={12}>
          <Stack
            direction={"row"}
            flexWrap={"wrap"}
            justifyContent={"space-between"}
            alignItems="center"
          >
            <Typography variant="h6">Logs</Typography>
          </Stack>
        </Grid>
        <Grid item md={12} xs={12}>
          <Paper variant="outlined" sx={{ padding: "1rem" }}>
            <Grid container spacing={0.8}>
              <Grid item md={3} xs={12}>
                <FormControl sx={{ width: "100%" }}>
                  <InputLabel id="resource-code-label">Resource</InputLabel>
                  <Select
                    labelId="resource-code-label"
                    id="resource-code-select-box"
                    multiple
                    value={filterState?.resourceCodes}
                    onChange={(e) => {
                      setFilterState((prev: FilterLog) => {
                        return {
                          ...prev,
                          resourceCodes: [...e.target.value],
                        };
                      });
                    }}
                    input={<OutlinedInput label="Resource" />}
                    renderValue={(selected) => selected.join(", ")}
                    MenuProps={MenuProps}
                  >
                    {resourceCodes?.data?.map((resourceCode) => (
                      <MenuItem key={resourceCode} value={resourceCode}>
                        <Checkbox
                          checked={
                            filterState.resourceCodes.indexOf(resourceCode) > -1
                          }
                        />
                        <ListItemText primary={resourceCode} />
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Grid>
              <Grid item md={3} xs={12}>
                <FormControl sx={{ width: "100%" }}>
                  <InputLabel id="controller-code-label">Controller</InputLabel>
                  <Select
                    labelId="controller-code-label"
                    id="controller-code-select-box"
                    multiple
                    value={filterState?.controllerCodes}
                    onChange={(e) =>
                      setFilterState((prev: FilterLog) => {
                        return {
                          ...prev,
                          controllerCodes: [
                            ...prev.controllerCodes,
                            ...e.target.value,
                          ],
                        };
                      })
                    }
                    input={<OutlinedInput label="Controller" />}
                    renderValue={(selected) => selected.join(", ")}
                    MenuProps={MenuProps}
                  >
                    {controllerCodes?.data?.map((controllerCode) => (
                      <MenuItem key={controllerCode} value={controllerCode}>
                        <Checkbox
                          checked={
                            filterState.controllerCodes.indexOf(
                              controllerCode
                            ) > -1
                          }
                        />
                        <ListItemText primary={controllerCode} />
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Grid>
            </Grid>
          </Paper>
        </Grid>
        <Grid item md={12} xs={12}>
          <Paper variant="outlined" sx={{ padding: "1rem" }}>
            <Grid container spacing={0.8}>
              <Grid item md={12} sm={12} sx={{ width: "100%" }}>
                <div style={{ width: "100%", height: 250 }}>
                  <ResponsiveContainer width="100%" height="100%">
                    <BarChart
                      width={500}
                      height={300}
                      data={requestsByResourceCode?.data as any}
                      margin={{
                        top: 5,
                        right: 5,
                        left: 5,
                        bottom: 5,
                      }}
                    >
                      <CartesianGrid strokeDasharray="3 3" />
                      <XAxis name="key" dataKey="key" />
                      <YAxis
                        label={{
                          value: "Requests",
                          angle: -90,
                          position: "insideBottomLeft",
                        }}
                      />
                      <CTooltip />
                      <Legend />
                      <Bar name="Endpoint" dataKey="value" fill="#8884d8">
                        {ColorUtilities.Colors.map((entry, index) => (
                          <Cell
                            key={`cell-${index}`}
                            fill={
                              ColorUtilities.Colors[
                                index % ColorUtilities.Colors.length
                              ]
                            }
                          />
                        ))}
                      </Bar>
                    </BarChart>
                  </ResponsiveContainer>
                </div>
              </Grid>
            </Grid>
          </Paper>
        </Grid>
        <Grid item md={12}>
          <Paper variant="outlined" sx={{ padding: "1rem" }}>
            <Grid container spacing={0.8}>
              <Grid item md={12}>
                <TableContainer
                  component={Paper}
                  sx={{ height: 400, overflow: "auto" }}
                >
                  <InfiniteScroll
                    height={"350px"}
                    dataLength={logsInfo.totalItems}
                    next={() => {
                      setFilterState((prev) => {
                        return {
                          ...prev,
                          page: prev.page + 1,
                        };
                      });
                    }}
                    hasMore={logsInfo.hasMore}
                    loader={
                      <Box
                        sx={{
                          width: "100%",
                          display: "flex",
                          justifyContent: "center",
                        }}
                      >
                        <CircularProgress />
                      </Box>
                    }
                    endMessage={
                      <Box
                        sx={{
                          margin: "1rem",
                          width: "100%",
                          display: "flex",
                          justifyContent: "center",
                        }}
                      >
                        No more items to load
                      </Box>
                    }
                  >
                    <Table stickyHeader size="small" aria-label="a dense table">
                      <TableHead>
                        <TableRow>
                          <TableCell>Resource</TableCell>
                          <TableCell>Action</TableCell>
                          <TableCell>Controller</TableCell>
                          <TableCell>Host</TableCell>
                          <TableCell>IP</TableCell>
                          <TableCell>Method</TableCell>
                          <TableCell>Path</TableCell>
                          <TableCell>Protocol</TableCell>
                          <TableCell>Port</TableCell>
                          <TableCell>Recorded On</TableCell>
                          <TableCell>Trace Identifier</TableCell>
                          <TableCell>User Agent</TableCell>
                          <TableCell>Scheme</TableCell>
                        </TableRow>
                      </TableHead>
                      <TableBody>
                        {logsInfo.logItems.map((row, index) => (
                          <TableRow
                            key={`KEY_${row.id}}`}
                            sx={{
                              "&:last-child td, &:last-child th": { border: 0 },
                            }}
                          >
                            <TableCell>{row.resourceCode}</TableCell>
                            <TableCell>{row.action}</TableCell>
                            <TableCell>{row.controller}</TableCell>
                            <TableCell>{row.host}</TableCell>
                            <TableCell>{row.ip}</TableCell>
                            <TableCell>{row.method}</TableCell>
                            <TableCell>{row.path}</TableCell>
                            <TableCell>{row.protocol}</TableCell>
                            <TableCell>{row.port}</TableCell>

                            <TableCell>{row.recordedOn}</TableCell>
                            <TableCell>{row.traceIdentifier}</TableCell>
                            <TableCell>{row.userAgent}</TableCell>
                            <TableCell>{row.scheme}</TableCell>
                          </TableRow>
                        ))}
                      </TableBody>
                    </Table>
                  </InfiniteScroll>
                </TableContainer>
              </Grid>
            </Grid>
          </Paper>
        </Grid>
      </Grid>
    </AppLazyLoader>
  );
};

export default ViewLogs;
