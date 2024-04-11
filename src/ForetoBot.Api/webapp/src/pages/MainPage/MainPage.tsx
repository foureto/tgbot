import React from "react";
import { useUnit } from "effector-react";
import { $data, weatherRequested } from "./mainPage.store";
import { Button } from "antd";
import { $app } from "@stores/app.store";

const MainPage: React.FC = () => {
  const { weather, loading } = useUnit($data);
  const { themeParams, theme } = useUnit($app);

  return (
    <div>
      <h3>Test page {loading ? "is loading..." : ""}</h3>
      <Button onClick={() => weatherRequested()}>Click me</Button>
      {theme}
      {JSON.stringify(themeParams)}
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
