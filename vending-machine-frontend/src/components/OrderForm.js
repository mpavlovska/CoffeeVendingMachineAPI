import React, { useEffect, useState } from "react";
import { getCoffeeTypes, getCustomizations, postOrder } from "../api";
import CoffeeTypeSelector from "./CoffeeTypeSelector";
import CustomizationSelector from "./CustomizationSelector";
import CoffeeOrderModal from "./CoffeeOrderModal";
import { Button, CircularProgress } from "@mui/material";

export default function OrderForm() {
  const [coffeeTypes, setCoffeeTypes] = useState([]);
  const [customizations, setCustomizations] = useState([]);
  const [selectedCoffee, setSelectedCoffee] = useState("");
  const [selectedCustomizations, setSelectedCustomizations] = useState([]);
  const [loading, setLoading] = useState(false);

  const [modalOpen, setModalOpen] = useState(false);
  const [brewProgress, setBrewProgress] = useState(0);
  const [showEnjoyMessage, setShowEnjoyMessage] = useState(false);

  useEffect(() => {
    getCoffeeTypes().then(setCoffeeTypes);
    getCustomizations().then(setCustomizations);
  }, []);

  const submit = async () => {
    try {
      setLoading(true);

      const coffee = coffeeTypes.find((c) => c.id === selectedCoffee);
      const payload = { coffeeTypeId: coffee.id, externalCoffeeName: coffee?.name, customizationIds: selectedCustomizations };

      await postOrder(payload);

      setModalOpen(true);
      setBrewProgress(0);
      setShowEnjoyMessage(false);

      const brewInterval = setInterval(() => {
        setBrewProgress((prev) => {
          if (prev >= 100) {
            clearInterval(brewInterval);
            setTimeout(() => {
              setShowEnjoyMessage(true);
              setTimeout(() => {
                setModalOpen(false);
                setSelectedCoffee("");
                setSelectedCustomizations([]);
              }, 1500);
            }, 500);
            return 100;
          }
          return prev + 5;
        });
      }, 150);
    } catch (err) {
      alert("Error creating order.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <CoffeeTypeSelector
        coffeeTypes={coffeeTypes}
        selected={selectedCoffee}
        onChange={setSelectedCoffee}
      />
      <CustomizationSelector
        customizations={customizations}
        selected={selectedCustomizations}
        onChange={setSelectedCustomizations}
      />

      <Button
        variant="contained"
        fullWidth
        disabled={!selectedCoffee || loading}
        onClick={submit}
        sx={{
          mt: 4,
          py: 1.5,
          fontSize: "1rem",
          backgroundColor: "#d7a86e",
          color: "#fff",
          borderRadius: 3,
          boxShadow: "0 4px 12px rgba(0,0,0,0.2)",
          ":hover": {
            backgroundColor: "#c68c5d",
          },
          ":disabled": {
            backgroundColor: "#ccc",
          },
        }}
      >
        {loading ? <CircularProgress size={24} color="inherit" /> : "Order Coffee"}
      </Button>

      <CoffeeOrderModal
        open={modalOpen}
        coffeeName={coffeeTypes.find((c) => c.id === selectedCoffee)?.name || ""}
        customizationCount={selectedCustomizations.length}
        progress={brewProgress}
        showEnjoyMessage={showEnjoyMessage}
        onClose={() => setModalOpen(false)}
      />
    </>
  );
}
