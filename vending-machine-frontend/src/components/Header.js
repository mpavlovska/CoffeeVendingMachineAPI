import React from "react";
import { Box, Typography, Container } from "@mui/material";

export default function Header() {
  return (
    <Box
      sx={{
        backgroundSize: "cover",
        backgroundPosition: "center",
        py: 4,
        px: 2,
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
      }}
    >
      <Container maxWidth="md">
        <Box
          sx={{
            backgroundColor: "rgba(0, 0, 0, 0.6)",
            borderRadius: 2,
            p: 4,
            textAlign: "center",
            color: "#fff",
            boxShadow: 4,
          }}
        >
          <Typography
            variant="h3"
            component="h1"
            fontFamily="'Lobster'"
            fontWeight="bold"
          >
            Discover the Art of Perfect Coffee
          </Typography>
          <Typography variant="subtitle1" sx={{ mt: 1 }}>
            Choose your type. Customize it your way.
          </Typography>
        </Box>
      </Container>
    </Box>
  );
}
