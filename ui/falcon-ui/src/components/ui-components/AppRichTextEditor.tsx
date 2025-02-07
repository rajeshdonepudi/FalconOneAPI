import { useTheme } from "@mui/material";
import { MouseEvent, forwardRef, useImperativeHandle } from "react";
import MenuControlsContainer from "@mui-tiptap-controls/MenuControlsContainer";
import MenuSelectHeading from "@mui-tiptap-controls/MenuSelectHeading";
import MenuDivider from "@mui-tiptap/MenuDivider";
import MenuButtonBold from "@mui-tiptap-controls/MenuButtonBold";
import MenuButtonItalic from "@mui-tiptap-controls/MenuButtonItalic";
import { StarterKit } from "@tiptap/starter-kit";
import RichTextEditorProvider from "@mui-tiptap/RichTextEditorProvider";
import RichTextField from "@mui-tiptap/RichTextField";
import { useEditor } from "@tiptap/react";
import MenuButtonBulletedList from "@mui-tiptap-controls/MenuButtonBulletedList";
import MenuButtonTextColor from "@mui-tiptap-controls/MenuButtonTextColor";
import MenuButtonCode from "@mui-tiptap-controls/MenuButtonCode";
import MenuButtonCodeBlock from "@mui-tiptap-controls/MenuButtonCodeBlock";
import MenuButtonUnderline from "@mui-tiptap-controls/MenuButtonUnderline";
import MenuButtonTaskList from "@mui-tiptap-controls/MenuButtonTaskList";
import MenuButtonStrikethrough from "@mui-tiptap-controls/MenuButtonStrikethrough";
import MenuButtonRedo from "@mui-tiptap-controls/MenuButtonRedo";
import MenuButtonUndo from "@mui-tiptap-controls/MenuButtonUndo";
import MenuButtonHighlightColor from "@mui-tiptap-controls/MenuButtonHighlightColor";
import MenuButtonHorizontalRule from "@mui-tiptap-controls/MenuButtonHorizontalRule";
import MenuSelectTextAlign from "@mui-tiptap-controls/MenuSelectTextAlign";
import MenuSelectFontFamily from "@mui-tiptap-controls/MenuSelectFontFamily";
import FontFamily from "@tiptap/extension-font-family";
import Color from "@tiptap/extension-color";
import CodeBlock from "@tiptap/extension-code-block";
import Text from "@tiptap/extension-text";
import TextStyle from "@tiptap/extension-text-style";
import Highlight from "@tiptap/extension-highlight";
import TaskList from "@tiptap/extension-task-list";
import TaskItem from "@tiptap/extension-task-item";
import Underline from "@tiptap/extension-underline";
import TextAlign from "@tiptap/extension-text-align";
import Dropcursor from "@tiptap/extension-dropcursor";
import Image from "@tiptap/extension-image";
import MenuButtonAddImage from "@mui-tiptap-controls/MenuButtonAddImage";
import MenuButtonImageUpload from "@mui-tiptap-controls/MenuButtonImageUpload";
import { ImageNodeAttributes } from "@mui-tiptap/utils/images";
import Link from "@tiptap/extension-link";
import Table from "@tiptap/extension-table";
import TableCell from "@tiptap/extension-table-cell";
import TableHeader from "@tiptap/extension-table-header";
import TableRow from "@tiptap/extension-table-row";
import MenuButtonEditLink from "@mui-tiptap-controls/MenuButtonEditLink";
import LinkBubbleMenu from "@mui-tiptap/LinkBubbleMenu";
import TableBubbleMenu from "@mui-tiptap/TableBubbleMenu";
import LinkBubbleMenuHandler from "@mui-tiptap/extensions/LinkBubbleMenuHandler";
import MenuButtonAddTable from "@mui-tiptap-controls/MenuButtonAddTable";
import MenuSelectFontSize from "@mui-tiptap/controls/MenuSelectFontSize";
import FontSize from "@mui-tiptap/extensions/FontSize";
import MenuButtonSubscript from "@mui-tiptap-controls/MenuButtonSubscript";
import MenuButtonSuperscript from "@mui-tiptap-controls/MenuButtonSuperscript";
import { isTouchDevice } from "@mui-tiptap/utils/platform";
import MenuButtonIndent from "@mui-tiptap-controls/MenuButtonIndent";
import MenuButtonUnindent from "@mui-tiptap-controls/MenuButtonUnindent";
import { Subscript } from "@tiptap/extension-subscript";
import { Superscript } from "@tiptap/extension-superscript";

const CustomSubscript = Subscript.extend({
  excludes: "superscript",
});

const CustomSuperscript = Superscript.extend({
  excludes: "subscript",
});

const CustomLinkExtension = Link.extend({
  inclusive: false,
});

const AppRichTextEditor = forwardRef((props: any, ref) => {
  const theme = useTheme();

  useImperativeHandle(ref, () => {
    return {
      editor,
    };
  });

  const editor = useEditor({
    onUpdate({ editor }) {
      props?.onEditorContentUpdate(editor.getHTML());
    },
    extensions: [
      StarterKit,
      FontFamily,
      Color,
      Text,
      Image,
      Dropcursor,
      TextStyle,
      CodeBlock,
      LinkBubbleMenuHandler,
      Highlight.configure({ multicolor: true }),
      TaskList,
      CustomSubscript,
      CustomSuperscript,
      TaskItem.configure({
        nested: true,
      }),
      TextAlign.configure({
        types: ["heading", "paragraph"],
      }),
      Underline,
      CustomLinkExtension.configure({
        autolink: true,
        linkOnPaste: true,
        openOnClick: false,
      }),
      FontSize,
      Table.configure({
        resizable: true,
      }),
      TableRow,
      TableHeader,
      TableCell,
    ],
    content: props?.content ?? "",
    editable: props?.editable ? true : false,
  });
  return (
    <RichTextEditorProvider editor={editor}>
      <RichTextField
        controls={
          <MenuControlsContainer>
            <MenuSelectHeading />
            <MenuDivider />
            <MenuButtonBold />
            <MenuButtonItalic />
            <MenuButtonBulletedList />
            <MenuButtonTextColor />
            <MenuButtonCode />
            <MenuButtonCodeBlock />
            <MenuButtonAddTable />
            {isTouchDevice() && (
              <>
                <MenuButtonIndent />

                <MenuButtonUnindent />
              </>
            )}
            <MenuSelectFontSize />
            <MenuSelectFontFamily
              options={[
                { label: "Monospace", value: "monospace" },
                {
                  label: "Ubuntu",
                  value: "ubuntu",
                },
                {
                  label: "Work Sans",
                  value: "Work Sans",
                },
                {
                  label: "Times New Roman",
                  value: "Times New Roman",
                },
                {
                  label: "Orbitron",
                  value: "Orbitron",
                },
                {
                  label: "DM Sans",
                  value: "DM Sans",
                },
              ]}
            />
            <MenuButtonAddImage
              onClick={function (
                event: MouseEvent<HTMLElement, globalThis.MouseEvent>,
                value: any
              ): void {
                throw new Error("Function not implemented.");
              }}
            />
            <MenuButtonImageUpload
              onUploadFiles={function (
                files: File[]
              ): ImageNodeAttributes[] | Promise<ImageNodeAttributes[]> {
                throw new Error("Function not implemented.");
              }}
            />
            <MenuButtonRedo />
            <MenuButtonUndo />
            <MenuButtonHighlightColor
              swatchColors={[
                { value: "#595959", label: "Dark grey" },
                { value: "#dddddd", label: "Light grey" },
                { value: "#ffa6a6", label: "Light red" },
                { value: "#ffd699", label: "Light orange" },
                { value: "#ffff00", label: "Yellow" },
                { value: "#99cc99", label: "Light green" },
                { value: "#90c6ff", label: "Light blue" },
                { value: "#8085e9", label: "Light purple" },
              ]}
            />{" "}
            <MenuButtonHorizontalRule />
            <MenuButtonStrikethrough />
            <MenuButtonTaskList />
            <MenuSelectTextAlign />
            <MenuButtonUnderline />
            <MenuButtonEditLink />
            <MenuButtonSubscript />
            <MenuButtonSuperscript />
            <MenuButtonTextColor
              defaultTextColor={theme.palette.text.primary}
              swatchColors={[
                { value: "#000000", label: "Black" },
                { value: "#ffffff", label: "White" },
                { value: "#888888", label: "Grey" },
                { value: "#ff0000", label: "Red" },
                { value: "#ff9900", label: "Orange" },
                { value: "#ffff00", label: "Yellow" },
                { value: "#00d000", label: "Green" },
                { value: "#0000ff", label: "Blue" },
              ]}
            />
          </MenuControlsContainer>
        }
      />
      <LinkBubbleMenu />
      <TableBubbleMenu />
    </RichTextEditorProvider>
  );
});

export default AppRichTextEditor;
