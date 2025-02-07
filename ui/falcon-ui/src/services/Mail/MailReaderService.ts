import { createApi } from "@reduxjs/toolkit/query/react";
import AuthorizedBaseQuery from "../AuthorizedBaseQuery";
import { ApiResponse } from "@/models/Common/ApiResponse";
import {
  Email,
  EmailMessageItem,
  GetMailRequest,
  ReadEmailMessage,
} from "@/models/Mails/GetMailRequest";
import MailReaderEndpoints from "@/endpoints/Mail/MailReaderEndpoints";

export const mailReaderAPI = createApi({
  reducerPath: "mailReaderAPI",
  tagTypes: [],
  baseQuery: AuthorizedBaseQuery,
  endpoints: (builder) => ({
    getEmailMessage: builder.mutation<ApiResponse<Email>, GetMailRequest>({
      query: (payload: GetMailRequest) => ({
        url: MailReaderEndpoints.getMailMessage,
        method: "POST",
        body: payload,
      }),
    }),
    downloadAttachment: builder.query({
      query: (payload) => ({
        url: MailReaderEndpoints.downloadAttachment(payload),
        responseHandler: (response) => response.blob(), // Handle response as a binary blob
      }),
    }),
    readEmailMessage: builder.mutation<
      ApiResponse<EmailMessageItem>,
      ReadEmailMessage
    >({
      query: (payload: ReadEmailMessage) => ({
        url: MailReaderEndpoints.readEmailMessage,
        method: "POST",
        body: payload,
      }),
    }),
    getEmailMessages: builder.mutation<
      ApiResponse<EmailMessageItem[]>,
      GetMailRequest
    >({
      query: (payload: GetMailRequest) => ({
        url: MailReaderEndpoints.getMailMessages,
        method: "POST",
        body: payload,
      }),
    }),
  }),
});

export const {
  useGetEmailMessageMutation,
  useLazyDownloadAttachmentQuery,
  useReadEmailMessageMutation,
  useGetEmailMessagesMutation,
} = mailReaderAPI;
