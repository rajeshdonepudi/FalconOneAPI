const isNullOrEmptyOrWhitespace = (value: string): boolean => {
  return !value || !value.trim();
};

const addEllipsis = (input: string, length: number) => {
  if (input.length <= 3) {
    return input; // No need to add ellipsis if string is already 3 characters or less
  } else {
    return input.substring(0, length) + "...";
  }
};

export default {
  isNullOrEmptyOrWhitespace,
  addEllipsis,
};
