import React from "react";
import { Avatar, Card, Col, Row } from "antd";
import { AccountRow } from "@models/Account";
import "./AccountItem.scss";

export interface AccountItemProps {
  account: AccountRow;
}

const AccountItem: React.FC<AccountItemProps> = ({ account }) => {
  if (!account) {
    return <>no acc</>;
  }

  return (
    <Row>
      <Col span={24}>
        <Card size="small" hoverable bordered style={{ width: "100%" }}>
          <Card.Meta
            title={
              <Row>
                <Col span={12}>
                  <div>{account.title}</div>
                  <div className="account-item-hint">{account.description}</div>
                </Col>
                <Col className="account-balance" span={12}>
                  <div>
                    {account.balance.amount} {account.balance.code}
                  </div>
                  <div className="account-item-hint">
                    {account.fiatBalance.amount} {account.fiatBalance.code}
                  </div>
                </Col>
              </Row>
            }
            avatar={
              <div className="account-icon">
                <Avatar src={account.icon} alt="icon" />
              </div>
            }
          />
        </Card>
      </Col>
    </Row>
  );
};

export default AccountItem;
