import React from "react";
import "./GlobalLoader.scss";

export interface GlobalErrorProps {
  message?: string;
  size?: string;
}

const GlobalLoader: React.FC<GlobalErrorProps> = ({ message, size }) => {
  const style = size ? { width: size, height: size } : {};
  return (
    <div className="e-container">
      <div className="square" style={style}>
        <span></span>
        <span></span>
        <span></span>
        <div className="e-message">{message}</div>
      </div>
    </div>
  );
};

export default GlobalLoader;
