import { Button } from "antd";
import React from "react";

export interface CommonButtonProps
  extends React.InputHTMLAttributes<HTMLButtonElement> {
  loading: boolean;
  children?: React.ReactNode;
}

const CommonButton: React.FC<CommonButtonProps> = (props) => {
  const { children, loading } = props;
  return <Button loading={loading}>{children}</Button>;
};
