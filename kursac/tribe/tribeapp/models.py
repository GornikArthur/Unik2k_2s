from typing import Optional

from sqlalchemy import ForeignKey, String, BigInteger
from sqlalchemy.orm import Mapped, DeclarativeBase, mapped_column
from sqlalchemy.ext.asyncio import AsyncAttrs, async_sessionmaker, create_async_engine
from sqlalchemy.orm import sessionmaker

DATABASE_URL = "postgresql+asyncpg://postgres:leguska4@localhost:5432/TribeAppDB"

# Создаем асинхронный движок подключения
engine = create_async_engine(DATABASE_URL, echo=True)

# Настраиваем сессию
async_session = sessionmaker(
    bind=engine,
    expire_on_commit=False
)

class Base(AsyncAttrs, DeclarativeBase):
    pass

class User(Base):
    __tablename__ = 'users'
    id: Mapped[int] = mapped_column(primary_key=True)
    tg_id: Mapped[int] = mapped_column(BigInteger)
    bio: Mapped[str] = mapped_column(String(500))
    location: Mapped[str] = mapped_column(String(128))
    gender: Mapped[bool] = mapped_column(default=True)

class Interest(Base):
    __tablename__ = 'interests'
    id: Mapped[int] = mapped_column(primary_key=True)
    title: Mapped[str] = mapped_column(String(128))
    description: Mapped[Optional[str]] = mapped_column(String(500), nullable=True)
    user: Mapped[int] = mapped_column(ForeignKey('users.id', ondelete='CASCADE'))

async def init_db():
    async with engine.begin() as conn:
        await conn.run_sync(Base.metadata.create_all)