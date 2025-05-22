import React from "react";
import {
  Card,
  CardActionArea,
  CardMedia,
  CardContent,
  Typography,
  Grid,
  Box,
} from "@mui/material";

export default function CoffeeTypeSelector({ coffeeTypes, selected, onChange }) {
  return (
    <Grid container spacing={3} justifyContent="center">
      {coffeeTypes.map((coffee) => {
        const isSelected = selected === coffee.id;

        return (
          <Grid item key={coffee.id}>
            <Card
              onClick={() => onChange(coffee.id)}
              sx={{
                width: 180,
                borderRadius: 4,
                boxShadow: isSelected ? 10 : 2,
                transform: isSelected ? "scale(1.05)" : "scale(1)",
                border: isSelected
                  ? "3px solid #ffcc80"
                  : "1px solid #ddd",
                backgroundColor: isSelected ? "#fff3e0" : "#fefae0",
                transition: "all 0.25s ease-in-out",
                cursor: "pointer",
                ":hover": {
                  boxShadow: 6,
                  transform: "scale(1.05)",
                },
              }}
            >
              <CardActionArea>
                <CardMedia
                  component="img"
                  height="160"
                  image={`/img/coffees/${coffee.name
                    .toLowerCase()
                    .replace(/ /g, "_")}.png`}
                  alt={coffee.name}
                  sx={{ objectFit: "contain", p: 1 }}
                />
                <CardContent sx={{ textAlign: "center", pb: 1 }}>
                  <Typography variant="subtitle1" fontWeight="bold">
                    {coffee.name}
                  </Typography>

                  {isSelected && (
                    <Typography
                      variant="body2"
                      sx={{
                        mt: 1,
                        color: "#6d4c41",
                        fontFamily: "'Lora', serif",
                      }}
                    >
                      {coffee.description}
                    </Typography>
                  )}
                </CardContent>
              </CardActionArea>
            </Card>
          </Grid>
        );
      })}
    </Grid>
  );
}
