import { ExpenseTypeEnum } from "@/enumerations/ExpenseManagement/ExpenseTypeEnum";

export interface AddExpense {
  amount: number;
  type: ExpenseTypeEnum;
  description: string;
  categoryId: string | null;
}
