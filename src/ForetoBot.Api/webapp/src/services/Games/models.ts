import { FileContent, TextContent } from "@services/commons";

export interface DomanCategoriesFilter {
  isActive?: boolean;
}

export interface DomanCategory {
  id: number;
  order: number;
  name: TextContent;
  description: TextContent;
  label: FileContent;
}
