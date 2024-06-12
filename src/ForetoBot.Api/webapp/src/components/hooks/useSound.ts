import { useRef, useEffect, useCallback } from "react";

export const useSound = (audioSource: string | undefined) => {
  const soundRef = useRef<HTMLAudioElement>();

  useEffect(() => {
    soundRef.current = new Audio(audioSource);
  }, [audioSource]);

  const playSound = useCallback(() => {
    soundRef.current?.play();
  }, [audioSource]);

  const pauseSound = useCallback(() => {
    soundRef.current?.pause();
  }, [audioSource]);

  return {
    playSound,
    pauseSound,
  };
};
