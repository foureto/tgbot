import {defineConfig} from "vite";
import react from "@vitejs/plugin-react";

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react()],
    envDir: "config/env",
    publicDir: "src/static",
    server: {
        host: true,
        port: "4000",
    },
    resolve: {
        alias: {
            "@components": "/src/components",
            "@styles": "/src/styles",
            "@pages": "/src/pages",
        },
    },
});
