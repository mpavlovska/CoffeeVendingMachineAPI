import React from "react";
import {
  FormGroup,
  FormControlLabel,
  Checkbox,
  Typography,
  Box,
  Paper,
} from "@mui/material";

export default function CustomizationSelector({ customizations, selected, onChange }) {
  const toggle = (id) => {
    onChange(
      selected.includes(id)
        ? selected.filter((i) => i !== id)
        : [...selected, id]
    );
  };

  return (
    <Box mt={6} display="flex" justifyContent="center">
      <Paper
        elevation={6}
        sx={{
          backgroundColor: "rgba(0, 0, 0, 0.5)",
          padding: 3,
          borderRadius: 2,
          color: "#fff",
          width: "fit-content",
          minWidth: 260,
        }}
      >
        <Typography variant="h5" fontFamily="'Lobster'">
          Perfect it :
        </Typography>

        <FormGroup>
          {customizations.map((item) => {
            const iconPath = `/img/customizations/${item.name.toLowerCase().replace(/ /g, "_")}.png`;

            return (
              <FormControlLabel
                key={item.id}
                control={
                  <Checkbox
                    checked={selected.includes(item.id)}
                    onChange={() => toggle(item.id)}
                    sx={{
                      color: "#fff",
                      "&.Mui-checked": { color: "#ffe082" },
                    }}
                  />
                }
                label={
                  <Box display="flex" alignItems="center" gap={1}>
                    <img
                      src={iconPath}
                      alt={item.name}
                      width={24}
                      height={24}
                      style={{ borderRadius: "4px" }}
                    />
                    <span>{item.name}</span>
                  </Box>
                }
              />
            );
          })}
        </FormGroup>
      </Paper>
    </Box>
  );
}
