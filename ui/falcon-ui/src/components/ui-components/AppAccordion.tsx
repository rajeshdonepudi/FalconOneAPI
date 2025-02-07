import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  styled,
  Typography,
} from "@mui/material";
import ArrowDownwardIcon from "@mui/icons-material/ArrowDownward";

const Root = styled("div")(({ theme }) => ({
  width: "100%",
  ...theme.typography.subtitle2,
  color: theme.palette.text.secondary,
  "& > :not(style) ~ :not(style)": {
    marginTop: theme.spacing(2),
  },
}));
const AppAccordion = (props: {
  title?: string;
  content: any;
  showCustomTitle?: boolean;
  renderTitle?: any;
  id?: string;
}) => {
  return (
    <Accordion key={`accordion_key_${props.id}`}>
      <AccordionSummary
        expandIcon={<ArrowDownwardIcon />}
        aria-controls="panel1-content"
        id="panel1-header"
      >
        {props?.showCustomTitle ? (
          <Root>{props?.renderTitle}</Root>
        ) : (
          <Typography variant="subtitle2">{props.title}</Typography>
        )}
      </AccordionSummary>
      <AccordionDetails>{props?.content}</AccordionDetails>
    </Accordion>
  );
};

export default AppAccordion;
