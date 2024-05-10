import AccountItem from "@components/AccountItem/AccountItem";
import { AccountRow } from "@models/Account";
import React from "react";

const account: AccountRow = {
  id: "text",
  externalKey: "someKey",
  icon: "",
  title: "EUR account",
  description: "Personal current account",
  status: 10,
  balance: {
    amount: "123.12",
    code: "EUR",
  },
  fiatBalance: {
    amount: "143.12",
    code: "USD",
  }
};

const ComponentsPage: React.FC = () => {
  return (
    <div style={{ width: "100%" }}>
      <h4>Accounts</h4>
      <AccountItem account={account} />
    </div>
  );
};

export default ComponentsPage;
