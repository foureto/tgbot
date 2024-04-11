import React from "react";
import "./GlobalError.scss";

export interface GlobalErrorProps {
  message?: string;
}

const GlobalError: React.FC<GlobalErrorProps> = ({ message }) => {
  return (
    <div className="e-container">
      <div className="square">
        <span></span>
        <span></span>
        <span></span>
        <div className="e-message">{message}</div>
      </div>
    </div>
  );
};

export default GlobalError;
