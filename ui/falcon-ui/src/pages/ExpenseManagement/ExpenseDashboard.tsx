import React, { useState, useEffect } from "react";
import AppPage from "@/components/ui-components/AppPage";
import AppConstants from "@/constants/constants";
import CurrencyUtilities from "@/utilities/CurrencyUtilities";
import NavUtilities from "@/utilities/NavUtilities";
import ArrowForwardOutlinedIcon from "@mui/icons-material/ArrowForwardOutlined";
import {
  Button,
  Card,
  CardContent,
  Grid,
  Stack,
  Typography,
  Skeleton,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import {
  CartesianGrid,
  Cell,
  Label,
  Line,
  LineChart,
  Pie,
  PieChart,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import { useGetExpensesDashboardInfoQuery } from "@/services/ExpenseManagement/ExpenseService";

const ExpenseDashboard = () => {
  const navigate = useNavigate();
  const { data: expenseData, isFetching: isLoading } =
    useGetExpensesDashboardInfoQuery(null);

  const handleViewExpenses = () => {
    navigate(NavUtilities.ToSecureArea("expenses/my"));
  };

  const renderHeaderActions = () => (
    <Button onClick={handleViewExpenses} endIcon={<ArrowForwardOutlinedIcon />}>
      View my Expenses
    </Button>
  );

  const renderTotalExpensesCard = () => (
    <Card sx={{ height: "300px" }} variant="outlined">
      <CardContent sx={{ height: "100%" }}>
        <Stack
          alignItems="center"
          justifyContent="center"
          sx={{ height: "100%" }}
        >
          {isLoading ? (
            <Skeleton variant="text" width={100} height={50} />
          ) : (
            <Typography variant="h4">
              {CurrencyUtilities.formatIndianCurrency(
                Math.round(expenseData?.data.totalExpenses ?? 0)
              )}
            </Typography>
          )}
          <Typography variant="caption">Total Expenses</Typography>
        </Stack>
      </CardContent>
    </Card>
  );

  const renderExpenseChartCard = () => (
    <Card sx={{ height: "300px", minHeight: "300px" }} variant="outlined">
      <CardContent sx={{ height: "100%" }}>
        {isLoading ? (
          <Skeleton variant="rectangular" width="100%" height="100%" />
        ) : (expenseData?.data.expensesByType.length ?? 0) > 0 ? (
          <ResponsiveContainer width="100%" height="100%">
            <PieChart>
              <Pie
                dataKey="value"
                nameKey="key"
                data={expenseData?.data.expensesByType ?? []}
                cx="50%"
                innerRadius={40} // Adds inner radius for a doughnut effect
                cy="50%"
                outerRadius={80}
                fill="#8884d8"
                label={({ key, value }) => {
                  return `${key} - ${CurrencyUtilities.formatIndianCurrency(
                    value
                  )}`;
                }}
                isAnimationActive={true}
              >
                {expenseData?.data.expensesByType.map((entry, index) => (
                  <Cell
                    key={`cell-${index}`}
                    fill={
                      AppConstants.chartColors[
                        Math.floor(
                          Math.random() * AppConstants.chartColors.length
                        )
                      ]
                    }
                  />
                ))}
              </Pie>
            </PieChart>
          </ResponsiveContainer>
        ) : (
          <Stack
            alignItems="center"
            justifyContent="center"
            sx={{ height: "100%" }}
          >
            <Typography variant="subtitle2" color="textSecondary">
              No data available
            </Typography>
          </Stack>
        )}
        <Stack alignItems="center">
          <Typography variant="caption">Expenses by Type</Typography>
        </Stack>
      </CardContent>
    </Card>
  );

  const last30DaysExpensesChart = () => {
    return (
      <Card sx={{ height: "300px" }} variant="outlined">
        <CardContent
          sx={{ height: "100%", padding: AppConstants.layout.StandardPadding }}
        >
          {isLoading ? (
            <Skeleton variant="rectangular" width="100%" height="100%" />
          ) : (expenseData?.data.last30DaysExpenses.length ?? 0) > 0 ? (
            <ResponsiveContainer width="100%" height="100%">
              <LineChart
                height={300}
                data={expenseData?.data.last30DaysExpenses}
                margin={{ top: 20, right: 30, left: 60, bottom: 5 }}
              >
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="key" />
                <YAxis
                  tickFormatter={(value) =>
                    `${CurrencyUtilities.formatIndianCurrency(value)}`
                  }
                />
                <Tooltip
                  formatter={(value) => [
                    `${CurrencyUtilities.formatIndianCurrency(Number(value))}`,
                    "Expense",
                  ]}
                  labelFormatter={(label) => `Date: ${label}`}
                />
                <Line
                  isAnimationActive={true}
                  type="monotone"
                  dataKey="value"
                  stroke="#8884d8"
                >
                  <Label value="Total Expenses" position="top" offset={10} />
                </Line>
              </LineChart>
            </ResponsiveContainer>
          ) : (
            <Stack
              alignItems="center"
              justifyContent="center"
              sx={{ height: "100%" }}
            >
              <Typography variant="subtitle2" color="textSecondary">
                No data available
              </Typography>
            </Stack>
          )}
          <Stack alignItems="center">
            <Typography variant="caption">Expenses by Category</Typography>
          </Stack>
        </CardContent>
      </Card>
    );
  };

  const renderExpenseByCategoryChartCard = () => (
    <Card sx={{ height: "300px" }} variant="outlined">
      <CardContent sx={{ height: "100%" }}>
        {isLoading ? (
          <Skeleton variant="rectangular" width="100%" height="100%" />
        ) : (expenseData?.data.expensesByCategory.length ?? 0) > 0 ? (
          <ResponsiveContainer width="100%" height="100%">
            <PieChart>
              <Pie
                dataKey="value"
                nameKey="key"
                data={expenseData?.data.expensesByCategory ?? []}
                cx="50%"
                innerRadius={40} // Adds inner radius for a doughnut effect
                cy="50%"
                outerRadius={80}
                fill="#8884d8"
                label={({ key, value }) => {
                  return `${key} - ${CurrencyUtilities.formatIndianCurrency(
                    value
                  )}`;
                }}
                isAnimationActive={true}
              >
                {expenseData?.data.expensesByCategory.map((entry, index) => (
                  <Cell
                    key={`cell-${index}`}
                    fill={
                      AppConstants.chartColors[
                        Math.floor(
                          Math.random() * AppConstants.chartColors.length
                        )
                      ]
                    }
                  />
                ))}
              </Pie>
            </PieChart>
          </ResponsiveContainer>
        ) : (
          <Stack
            alignItems="center"
            justifyContent="center"
            sx={{ height: "100%" }}
          >
            <Typography variant="subtitle2" color="textSecondary">
              No data available
            </Typography>
          </Stack>
        )}
        <Stack alignItems="center">
          <Typography variant="caption">Expenses by Category</Typography>
        </Stack>
      </CardContent>
    </Card>
  );

  return (
    <AppPage
      title="Dashboard"
      rightHeaderActions={renderHeaderActions()}
      content={
        <Grid container spacing={AppConstants.layout.StandardSpacing}>
          <Grid item xs={12} md={3}>
            {renderTotalExpensesCard()}
          </Grid>
          <Grid item xs={12} md={9}>
            {renderExpenseChartCard()}
          </Grid>
          <Grid item xs={12} md={4}>
            {renderExpenseByCategoryChartCard()}
          </Grid>
          <Grid item xs={12} md={8}>
            {last30DaysExpensesChart()}
          </Grid>
        </Grid>
      }
    />
  );
};

export default ExpenseDashboard;
