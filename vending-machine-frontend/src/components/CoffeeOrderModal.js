import React from "react";
import { Modal, Box, Typography, LinearProgress } from "@mui/material";

export default function CoffeeOrderModal({
  open,
  coffeeName,
  customizationCount,
  progress,
  showEnjoyMessage,
  onClose,
}) {
  return (
    <Modal open={open}>
      <Box
        sx={{
          position: "absolute",
          top: "50%",
          left: "50%",
          transform: "translate(-50%, -50%)",
          bgcolor: "#fefae0",
          borderRadius: 3,
          p: 4,
          width: "90%",
          maxWidth: 400,
          textAlign: "center",
          boxShadow: 24,
        }}
      >
        <Typography variant="h5" fontWeight="bold" mb={2} color="black">
          {showEnjoyMessage ? "Enjoy your coffee! " : "Preparing your order..."}
        </Typography>

        {!showEnjoyMessage && (
          <>
            <Typography variant="body1" mb={2}>
              <strong>{coffeeName}</strong>
              <br />
              with {customizationCount} customization(s)
            </Typography>
            <LinearProgress
              variant="determinate"
              value={progress}
              sx={{ height: 10, borderRadius: 5 }}
            />
          </>
        )}
      </Box>
    </Modal>
  );
} 
