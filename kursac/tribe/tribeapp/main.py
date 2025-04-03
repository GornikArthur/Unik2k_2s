from contextlib import asynccontextmanager

from pydantic import BaseModel
from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from models import init_db
import requests as rq

app = FastAPI()

@asynccontextmanager
async def lifespan(app_:FastAPI):
    await init_db()
    print("Bot is ready")
    yield

app.add_middleware(
    CORSMiddleware,
    allow_origins=['*'],
    allow_credentials=True,
    allow_methods=['*'],
    allow_headers=['*'],
)

# @app.post("/users/")
# async def create_user(user: UserBase, db: db_dependency):
#     db_user = models.User(id=user.id, name=user.username)
#     db.add(db_user)
#     db.commit()
#     db.refresh(db_user)

@app.get("/api/iterests/{tg_id}")
async def interests(tg_id: int):
    user = await rq.add_user(tg_id)
    return await rq.get_interest(user.id)
