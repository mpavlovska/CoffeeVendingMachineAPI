import React from "react";
import { Container, Box } from "@mui/material";
import Header from "./components/Header";
import OrderForm from "./components/OrderForm";

function App() {
  return (
    <>
      <Header />
      <Container maxWidth="lg">
        <Box sx={{ mt: 4 }}>
          <OrderForm />
        </Box>
      </Container>
    </>
  );
}

export default App;
