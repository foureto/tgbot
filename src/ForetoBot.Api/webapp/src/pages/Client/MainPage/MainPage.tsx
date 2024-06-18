import React from "react";
import { useUnit } from "effector-react";
import { useInitData } from "@tma.js/sdk-react";
import { Button } from "antd";
import { $data, weatherRequested } from "./mainPage.store";

const MainPage: React.FC = () => {
  // const initparams = useInitData();
  const { weather, loading } = useUnit($data);

  return (
    <div>
      <h3>Test page {loading ? "is loading..." : ""}</h3>
      <Button onClick={() => weatherRequested()}>Click me</Button>

      <pre>
        <code>{JSON.stringify({}, null, " ")}</code>
      </pre>
      <div>
        {(weather ?? []).map((e, i) => (
          <div key={i}>{e.one}</div>
        ))}
      </div>
    </div>
  );
};

export default MainPage;
