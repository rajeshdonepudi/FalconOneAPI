import { Route } from "react-router-dom";
import Dashboard from "@/pages/Dashboard/Dashboard";
import DashboardLayout from "@/layouts/DashboardLayout";
import PageNotFound from "@/pages/Common/PageNotFound";
import ManageUsers from "@/pages/Users/ManageUsers";
import UserProfile from "@/pages/Users/UserProfile";
import RouteGuard from "@/guards/RouteGuard";
import ManageSiteTheme from "@/pages/Themes/ManageSiteTheme";
import ViewLogs from "@/pages/SystemLogs/ViewLogs";
import ComposeMail from "@/pages/Mail/ComposeMail";
import ViewUserEmails from "@/pages/Mail/ViewUserEmails";
import ViewMail from "@/pages/Mail/ViewMail";
import UserDirectory from "@/components/features/Users/UserDirectory";
import UsersReport from "@/pages/Reports/Users/UsersReport";
import ManageTenants from "@/pages/Tenant/ManageTenants";
import ViewRoles from "@/pages/Security/Roles/ViewRoles";
import ViewPermissions from "@/pages/Security/Permissions/ViewTenantPermissions";
import ViewSecurityGroups from "@/pages/Security/SecurityGroups/ViewSecurityGroups";
import ManageTenantPermissions from "@/pages/Security/Permissions/ManageTenantPermissions";
import ViewUserDetails from "@/pages/Users/ViewUserDetails";
import ViewTenantDetails from "@/pages/Tenant/ViewTenantDetails";
import ViewSecurityGroupDetails from "@/pages/Security/SecurityGroups/ViewSecurityGroupDetails";
import ViewRoleDetails from "@/pages/Security/Roles/ViewRoleDetails";
import Meeting from "@/pages/Meetings/MeetingLobby";
import Test from "@/TestComponent/Test";
import ViewTrainedModels from "@/pages/AI/ViewTrainedModels";
import AppPageV2 from "@/components/ui-components/AppPage";
import { Button } from "@mui/material";
import ViewQuestions from "@/pages/QuestionAndAnswers/ViewQuestions";
import ViewMailMessages from "@/pages/Mail/Reader/ViewMailMessages";
import ViewIndividualMail from "@/pages/Mail/Reader/ViewIndividualMail";
import AppAccessForbidden from "@/components/ui-components/AppAccessForbidden";
import ViewUserExpenses from "@/pages/ExpenseManagement/ViewUserExpenses";
import ExpenseDashboard from "@/pages/ExpenseManagement/ExpenseDashboard";
import ViewExpense from "@/pages/ExpenseManagement/ViewExpense";
import ViewExpenseCategories from "@/pages/ExpenseManagement/ViewExpenseCategories";
import Test3 from "@/TestComponent/Test3";
import VideoCall from "@/components/features/Video/VideoCall";
import OnboardTenant from "@/pages/Tenant/OnboardTenant";
import OrgChartComponent from "@/pages/Tenant/OrgChartComponent";
import ManageTenantPermissionsForm from "@/components/features/Security/Permissions/ManageTenantPermissionsForm";
import ManageTenantPermissionsNew from "@/pages/Security/Permissions/ManageTenantPermissionNew";

const DashboardRoutes = () => {
  return (
    <Route element={(<RouteGuard />) as any}>
      <Route path="secure" element={<DashboardLayout />}>
        <Route path="drawer" element={<Test3 />}></Route>
        <Route path="forbidden" element={<AppAccessForbidden />}></Route>
        <Route path="dashboard" element={<Dashboard />}></Route>
        <Route
          path="*"
          element={
            <PageNotFound url="https://assets8.lottiefiles.com/packages/lf20_xiebbQE7S1.json" />
          }
        ></Route>
        <Route path="users">
          <Route index element={<ManageUsers />}></Route>
          <Route path="view" element={<ViewUserDetails />}></Route>
        </Route>
        <Route path="manage-tenants">
          <Route path="onboard" element={<OnboardTenant />}></Route>
          <Route path="chart" element={<OrgChartComponent />}></Route>
          <Route index element={<ManageTenants />} />
          <Route path="view" element={<ViewTenantDetails />}></Route>
        </Route>
        <Route path="manage-theme" element={<ManageSiteTheme />}></Route>
        <Route path="profile" element={<UserProfile />}></Route>
        <Route path="user-directory" element={<UserDirectory />}></Route>
        <Route path="logs" element={<ViewLogs />}></Route>
        <Route path="meetings">
          <Route index element={<Meeting />}></Route>
          <Route path="create-join" element={<VideoCall />}></Route>
        </Route>
        <Route path="messages">
          <Route path="read" element={<ViewMailMessages />}></Route>
          <Route path="read-mail" element={<ViewIndividualMail />}></Route>
        </Route>
        <Route path="mails">
          <Route index element={<ViewUserEmails />}></Route>
          <Route path="compose" element={<ComposeMail />}></Route>
          <Route path="view/:id" element={<ViewMail />}></Route>
        </Route>
        <Route path="reports">
          <Route
            index
            path="users-created-overtime"
            element={<UsersReport />}
          ></Route>
        </Route>
        <Route path="security">
          <Route path="roles">
            <Route index element={<ViewRoles />}></Route>
            <Route path="edit" element={<ViewRoleDetails />}></Route>
          </Route>
          <Route path="permissions">
            <Route index element={<ViewPermissions />}></Route>
          </Route>
          <Route path="permissions-pool">
            <Route index element={<ManageTenantPermissions />}></Route>
            <Route path="new" element={<ManageTenantPermissionsNew />}></Route>
          </Route>
          <Route path="security-groups">
            <Route index element={<ViewSecurityGroups />}></Route>
            <Route path="view" element={<ViewSecurityGroupDetails />}></Route>
          </Route>
        </Route>

        <Route path="ai/trained-models">
          <Route index element={<ViewTrainedModels />}></Route>
        </Route>
        <Route path="survey">
          <Route path="questions" element={<ViewQuestions />}></Route>
        </Route>
        <Route path="expenses">
          <Route index element={<ExpenseDashboard />}></Route>
          <Route path="my" element={<ViewUserExpenses />}></Route>
          <Route path="view" element={<ViewExpense />}></Route>
          <Route path="categories" element={<ViewExpenseCategories />}></Route>
        </Route>
      </Route>
      <Route path="design" element={<DashboardLayout />}>
        <Route
          index
          element={
            <AppPageV2
              title={"Hello"}
              rightHeaderActions={
                <div>
                  <Button>eeee</Button>
                </div>
              }
              content={<h1>hello</h1>}
            />
          }
        />
      </Route>
    </Route>
  );
};

export default DashboardRoutes;
