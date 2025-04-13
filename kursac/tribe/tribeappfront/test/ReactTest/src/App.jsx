import { useState, useEffect  } from 'react'
import './App.css'

function App() {
  const [fruits, setFruits] = useState([]);
  const [newFruit, setNewFruit] = useState("");

  // Загрузка списка фруктов
  useEffect(() => {
    fetch("http://127.0.0.1:8000/fruits")
      .then((res) => res.json())
      .then((data) => setFruits(data.fruits));
  }, []);

  // Отправка нового фрукта
  const handleAddFruit = async () => {
    if (!newFruit.trim()) return;

    const response = await fetch("http://127.0.0.1:8000/fruits", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ name: newFruit }),
    });

    const addedFruit = await response.json();
    setFruits((prev) => [...prev, addedFruit]);
    setNewFruit("");
  };

  return (
    <div style={{ padding: "2rem" }}>
      <h1>Fruits List 🍎</h1>
      <ul>
        {fruits.map((fruit, index) => (
          <li key={index}>{fruit.name}</li>
        ))}
      </ul>

      <input type="text" value={newFruit} onChange={(e) => setNewFruit(e.target.value)} placeholder="Enter a fruit"/>
      <button onClick={handleAddFruit}>Add Fruit</button>
    </div>
  );
}

export default App;

