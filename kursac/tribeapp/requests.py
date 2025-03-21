from sqlalchemy import select, update, delete, func
from models import User, Interest, async_session
from pydantic import BaseModel, ConfigDict
from typing import List

class InterestSchema(BaseModel):
    id: int
    title: str
    description: str
    user_id: int

    model_config = ConfigDict(from_attributes=True)

async def add_user(tg_id):
    async with async_session() as session:
        user = await session.scalar(select(User).where(User.id == tg_id))
        if user:
            return user
        new_user = User(id=tg_id)
        session.add(new_user)
        await session.commit()
        await session.refresh(new_user)
        return new_user

async def get_interest(user_id):
    async with async_session() as session:
        interest = await session.scalars(select(Interest).where(Interest.user == user_id))
        serialized_interest = [InterestSchema.model_validate(t).model_dump() for t in interest]
        return serialized_interest