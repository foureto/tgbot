import { AmountValue } from "./Shared";

export interface AccountRow {
  id: string;
  title: string;
  description: string;
  status: number;
  externalKey: string;
  icon: string;
  balance: AmountValue;
  fiatBalance: AmountValue;
}

export interface AccountDetailsRow extends AccountRow {}
