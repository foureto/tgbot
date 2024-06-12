import React from "react";
import { Button, ButtonProps } from "antd";
import { useSound } from "@components/hooks/useSound";

export interface SoundButtonProps
  extends ButtonProps,
    React.RefAttributes<HTMLElement> {
  sound?: string;
}

const SoundButton: React.FC<SoundButtonProps> = ({ sound, ...props }) => {
  const { playSound } = useSound("/sounds/Пчела.mp3");

  const click = (e: any) => {
    playSound();
    if (props.onClick) props.onClick(e);
  };
  return <Button {...props} onClick={click}></Button>;
};

export default SoundButton;
