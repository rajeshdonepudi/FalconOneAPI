import React from "react";

interface AppLottieAnimationProps {
  lottieUrl: string;
  width?: string; // Optional props
  height?: string; // Optional props
}

const AppLottieAnimation: React.FC<AppLottieAnimationProps> = ({
  lottieUrl,
  width = "300px",
  height = "300px",
}) => {
  const loaderDimensions: React.CSSProperties = {
    width,
    height,
  };

  return (
    <lottie-player
      src={lottieUrl}
      background="transparent"
      speed="1"
      style={loaderDimensions}
      loop
      autoplay
    ></lottie-player>
  );
};

export default AppLottieAnimation;
