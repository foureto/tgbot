import { useUnit } from "effector-react";
import React from "react";
import { $data, weatherRequested } from "./mainPage.store";
import { Button } from "antd";

const MainPage: React.FC = () => {
  const { weather, loading } = useUnit($data);

  return (
    <div>
      <h3>Test page {loading ? "is loading..." : ""}</h3>
      <Button onClick={() => weatherRequested()}>Click me</Button>
      <div>
        {(weather ?? []).map((e, i) => (
          <div key={i}>
            {e.date} {e.summary} {e.value}
          </div>
        ))}
      </div>
    </div>
  );
};

export default MainPage;
