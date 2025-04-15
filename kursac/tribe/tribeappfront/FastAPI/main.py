from fastapi import FastAPI

from pydantic import BaseModel, HttpUrl
from fastapi.middleware.cors import CORSMiddleware
from typing import List, Optional
from fastapi import HTTPException

app = FastAPI()

app.add_middleware(
    CORSMiddleware,
    allow_origins=[
        "http://localhost:5173",
        "http://127.0.0.1:5173"
    ],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

class Location(BaseModel):
    Country: str
    City: str

class Interest(BaseModel):
    interest_id: int
    Title: str
    Description: str

class User(BaseModel):
    user_id: int
    ProfilePicUrl: str
    Name: str
    Age: int
    Location: Location
    TelegramLink: HttpUrl
    Interests: List[Interest]

class Users(BaseModel):
    users: List[User]

memory = {
  "users": [
    {
        "user_id": 1,
        "Name": "Artur",
        "Age": 25,
        "Location": { "Country": "Latvia", "City": "Riga" },
        "TelegramLink": "https://t.me/arturgornik",
        "ProfilePicUrl": "/img/Profile.png",
        "Interests": [
            {
                "interest_id": 1,
                "Title": "Artificial Intelligence",
                "Description": "Exploring the capabilities of AI in automation, creativity, and decision-making."
            },
            {
                "interest_id": 2,
                "Title": "Basketball",
                "Description": "Enjoys playing and watching basketball, especially NBA games."
            },
            {
                "interest_id": 3,
                "Title": "Philosophy",
                "Description": "Interested in existentialism, ethics, and the philosophy of mind."
            },
            {
                "interest_id": 4,
                "Title": "Traveling",
                "Description": "Loves discovering new cultures and meeting people around the world."
            }
        ]
    },
    {
        "user_id": 2,
        "Name": "Anton",
        "Age": 27,
        "Location": { "Country": "USA", "City": "Los-Angeles" },
        "TelegramLink": "https://t.me/razhnou",
        "ProfilePicUrl": "/img/Profile.png",
        "Interests": [
            {
                "interest_id": 1,
                "Title": "Cryptocurrency",
                "Description": "Follows crypto trends, blockchain innovations, and Web3 startups."
            },
            {
                "interest_id": 2,
                "Title": "Fitness",
                "Description": "Maintains an active lifestyle through gym workouts and healthy habits."
            },
            {
                "interest_id": 3,
                "Title": "Startups",
                "Description": "Passionate about building and scaling tech-driven businesses."
            },
            {
                "interest_id": 4,
                "Title": "Gaming",
                "Description": "Enjoys competitive online games and exploring game design mechanics."
            }
        ]
    },
    {
    "user_id": 3,
    "Name": "Elena",
    "Age": 23,
    "Location": { "Country": "Germany", "City": "Berlin" },
    "TelegramLink": "https://t.me/elena_dev",
    "ProfilePicUrl": "/img/Profile.png",
    "Interests": [
        {
            "interest_id": 1,
            "Title": "UI/UX Design",
            "Description": "Passionate about creating intuitive and beautiful digital interfaces."
        },
        {
            "interest_id": 2,
            "Title": "Painting",
            "Description": "Enjoys watercolor and acrylic painting in her free time."
        },
        {
            "interest_id": 3,
            "Title": "Yoga",
            "Description": "Practices yoga daily for mindfulness and physical health."
        },
        {
            "interest_id": 4,
            "Title": "Sustainability",
            "Description": "Advocates for eco-friendly lifestyle and environmental responsibility."
        }
    ]
    },
    {
        "user_id": 4,
        "Name": "Mark",
        "Age": 30,
        "Location": { "Country": "Canada", "City": "Toronto" },
        "TelegramLink": "https://t.me/mark_code",
        "ProfilePicUrl": "/img/Profile.png",
        "Interests": [
            {
                "interest_id": 1,
                "Title": "Machine Learning",
                "Description": "Enjoys building predictive models and analyzing data."
            },
            {
                "interest_id": 2,
                "Title": "Cooking",
                "Description": "Loves experimenting with new recipes and world cuisines."
            },
            {
                "interest_id": 3,
                "Title": "Snowboarding",
                "Description": "Hits the slopes every winter in the Canadian Rockies."
            },
            {
                "interest_id": 4,
                "Title": "Sci-Fi Books",
                "Description": "Fan of Isaac Asimov, Arthur C. Clarke, and Philip K. Dick."
            }
        ]
    },
    {
        "user_id": 5,
        "Name": "Sara",
        "Age": 22,
        "Location": { "Country": "Spain", "City": "Barcelona" },
        "TelegramLink": "https://t.me/sara_inspo",
        "ProfilePicUrl": "/img/Profile.png",
        "Interests": [
            {
                "interest_id": 1,
                "Title": "Photography",
                "Description": "Enjoys street and nature photography."
            },
            {
                "interest_id": 2,
                "Title": "Languages",
                "Description": "Learning French, German and Japanese."
            },
            {
                "interest_id": 3,
                "Title": "Dancing",
                "Description": "Loves salsa and modern dance classes."
            },
            {
                "interest_id": 4,
                "Title": "Volunteering",
                "Description": "Helps local NGOs with educational projects."
            }
        ]
    },
    {
        "user_id": 6,
        "Name": "David",
        "Age": 28,
        "Location": { "Country": "UK", "City": "London" },
        "TelegramLink": "https://t.me/davidtech",
        "ProfilePicUrl": "/img/Profile.png",
        "Interests": [
            {
                "interest_id": 1,
                "Title": "Blockchain",
                "Description": "Exploring decentralization and smart contracts."
            },
            {
                "interest_id": 2,
                "Title": "Football",
                "Description": "Plays in an amateur league every weekend."
            },
            {
                "interest_id": 3,
                "Title": "Podcasts",
                "Description": "Listens to tech and business podcasts daily."
            },
            {
                "interest_id": 4,
                "Title": "Coffee Brewing",
                "Description": "Enthusiast of specialty coffee and home brewing."
            }
        ]
    },
    {
        "user_id": 7,
        "Name": "Yuki",
        "Age": 26,
        "Location": { "Country": "Japan", "City": "Osaka" },
        "TelegramLink": "https://t.me/yuki_world",
        "ProfilePicUrl": "/img/Profile.png",
        "Interests": [
            {
                "interest_id": 1,
                "Title": "Anime",
                "Description": "Big fan of Studio Ghibli and classic series."
            },
            {
                "interest_id": 2,
                "Title": "Programming",
                "Description": "Loves building mobile apps with Flutter and Kotlin."
            },
            {
                "interest_id": 3,
                "Title": "Origami",
                "Description": "Practices traditional Japanese paper folding art."
            },
            {
                "interest_id": 4,
                "Title": "Travel Blogging",
                "Description": "Shares experiences and tips from global adventures."
            }
        ]
    }
  ]
}


my_user = memory["users"][3]

@app.get("/likes", response_model=Users)
def get_likes():
    return Users(users=memory["users"])

@app.get("/search/{user_id}", response_model=User)
def get_user_by_id(user_id: int):
    user = next((u for u in memory["users"] if u["user_id"] == user_id), None)
    if my_user["user_id"] == user["user_id"]:
        user = next((u for u in memory["users"] if u["user_id"] == user_id), None)
    if user is None:
        raise HTTPException(status_code=404, detail="User not found")
    return User(**user)

@app.get("/edit", response_model=User)
def get_my_user():
    return User(**my_user)

