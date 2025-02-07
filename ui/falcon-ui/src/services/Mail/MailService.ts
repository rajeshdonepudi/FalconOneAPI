import { createApi } from "@reduxjs/toolkit/query/react";
import AuthorizedBaseQuery from "../AuthorizedBaseQuery";
import { ApiResponse } from "@models/Common/ApiResponse";
import { PagedList } from "@/models/Common/PagedResponse";
import { MailListItem } from "@/models/Mails/MailListItem";
import { FilterMail } from "@/models/Mails/FilterMails";
import MailEndpoints from "@/endpoints/Mail/MailEndpoints";
import { NewMail } from "@/models/Mails/NewMail";
import { MailInfo } from "@/models/Mails/MailInfo";

export const mailsAPI = createApi({
  reducerPath: "mailsAPI",
  tagTypes: ["settings-by-type", "tenant-settings", "types"],
  baseQuery: AuthorizedBaseQuery,

  endpoints: (builder) => ({
    getUserSentMails: builder.query<
      ApiResponse<PagedList<MailListItem>>,
      FilterMail
    >({
      query: (payload: FilterMail) => ({
        url: MailEndpoints.getUserSentMails,
        method: "POST",
        body: payload,
      }),
      keepUnusedDataFor: 0,
    }),
    getUserReceivedMails: builder.query<
      ApiResponse<PagedList<MailListItem>>,
      FilterMail
    >({
      query: (payload: FilterMail) => ({
        url: MailEndpoints.getUserReceivedMails,
        method: "POST",
        body: payload,
      }),
      keepUnusedDataFor: 0,
    }),
    sendNewMail: builder.mutation<ApiResponse<boolean>, NewMail>({
      query: (payload: NewMail) => ({
        url: MailEndpoints.sendNewMail,
        method: "POST",
        body: payload,
      }),
    }),
    getMailInfo: builder.query<ApiResponse<MailInfo>, string>({
      query: (mailId: string) => ({
        url: MailEndpoints.getMailInfo(mailId),
        method: "GET",
      }),
      keepUnusedDataFor: 0,
    }),
  }),
});

export const {
  useGetUserReceivedMailsQuery,
  useSendNewMailMutation,
  useGetUserSentMailsQuery,
  useLazyGetMailInfoQuery,
} = mailsAPI;
