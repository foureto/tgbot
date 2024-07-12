import React from "react";
import { Menu } from "antd";
import { useNavigate } from "react-router-dom";

const SideMenu: React.FC = () => {
  const navigate = useNavigate();

  const items = [
    {
      key: "main",
      label: "Главная",
      path: "",
    },
    {
      key: "games",
      label: "Игры",
      children: [
        {
          key: "doman",
          label: "Карточки домана",
        },
      ],
    },
  ];

  const onClick = (menuItem: any) => {
    navigate(`/adm/${menuItem.keyPath.reverse().join("/")}`);
  };

  return (
    <Menu
      onClick={onClick}
      style={{
        width: "100%",
      }}
      defaultSelectedKeys={["main"]}
      mode="inline"
      items={items}
      theme="dark"
    />
  );
};

export default SideMenu;
