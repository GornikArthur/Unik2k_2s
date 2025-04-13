from fastapi import FastAPI

from pydantic import BaseModel
from fastapi.middleware.cors import CORSMiddleware
from typing import List

app = FastAPI()

# Разрешить CORS (иначе React не сможет обращаться к API)
app.add_middleware(
    CORSMiddleware,
    allow_origins=["http://localhost:5173"],  # адрес фронта
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

class Fruit(BaseModel):
    name: str

class Fruits(BaseModel):
    fruits: List[Fruit]

memory = {"fruits":[]}

@app.get("/fruits", response_model=Fruits)
def get_fruits():
    return Fruits(fruits=memory["fruits"])


@app.post("/fruits", response_model=Fruit)
def post_fruit(fruit: Fruit):
    memory["fruits"].append(fruit)
    return fruit
