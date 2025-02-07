import { createApi } from "@reduxjs/toolkit/query/react";
import AuthorizedBaseQuery from "../AuthorizedBaseQuery";
import { ApiResponse } from "@models/Common/ApiResponse";
import { TrainedModelFileInfo } from "@/models/AI/TrainedModelFileInfo";
import AIEndpoints from "@/endpoints/AI/AIEndpoints";

export const aiAPI = createApi({
  reducerPath: "aiAPI",
  tagTypes: ["all-trained-models"],
  baseQuery: AuthorizedBaseQuery,
  endpoints: (builder) => ({
    getAllTrainedModels: builder.query<
      ApiResponse<TrainedModelFileInfo[]>,
      null
    >({
      query: () => ({
        url: AIEndpoints.getAllTrainedModels,
        method: "GET",
      }),
      providesTags: ["all-trained-models"],
    }),
  }),
});

export const { useGetAllTrainedModelsQuery } = aiAPI;
