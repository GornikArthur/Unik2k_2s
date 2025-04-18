from sqlalchemy import String, Integer, BigInteger, ForeignKey
from sqlalchemy.orm import Mapped, mapped_column, relationship, DeclarativeBase
from sqlalchemy.ext.asyncio import create_async_engine, async_sessionmaker, AsyncAttrs
from typing import List

# URL подключения (обрати внимание на asyncpg)
URL_DATABASE = 'postgresql+asyncpg://postgres:mYqAqczCXkfKSPyCmHJpoPIekYaiImuN@tramway.proxy.rlwy.net:31843/railway'

# Асинхронный движок и сессия
engine = create_async_engine(url=URL_DATABASE, echo=True)
async_session = async_sessionmaker(bind=engine, expire_on_commit=False)

# Базовый класс для моделей
class Base(AsyncAttrs, DeclarativeBase):
    pass

# --- МОДЕЛИ ---

class Location(Base):
    __tablename__ = 'locations'

    location_id: Mapped[int] = mapped_column(primary_key=True)
    country: Mapped[str] = mapped_column(String(128))
    city: Mapped[str] = mapped_column(String(128))

    users: Mapped[List["User"]] = relationship("User", back_populates="location")


class User(Base):
    __tablename__ = 'users'

    user_id: Mapped[int] = mapped_column(primary_key=True)
    telegram_id: Mapped[int] = mapped_column(BigInteger)
    profile_pic_url: Mapped[str] = mapped_column(String(256))
    name: Mapped[str] = mapped_column(String(128))
    age: Mapped[int] = mapped_column(Integer)
    
    location_id: Mapped[int] = mapped_column(ForeignKey('locations.location_id'))

    location: Mapped["Location"] = relationship("Location", back_populates="users")
    interests: Mapped[List["Interest"]] = relationship("Interest", back_populates="user")


class Interest(Base):
    __tablename__ = 'interests'

    interest_id: Mapped[int] = mapped_column(primary_key=True)
    user_id: Mapped[int] = mapped_column(ForeignKey('users.user_id'))
    title: Mapped[str] = mapped_column(String(128))
    description: Mapped[str] = mapped_column(String(512))

    user: Mapped["User"] = relationship("User", back_populates="interests")

async def init_db():
    async  with engine.begin() as con:
        await con.run_sync(Base.metadata.create_all)