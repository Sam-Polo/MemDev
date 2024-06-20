from aiogram import F, Router
from aiogram.types import Message
from aiogram import types
from aiogram.filters import CommandStart, Command
import app.keyboards as kb
from aiogram.enums import ParseMode, ChatAction

router = Router()

@router.message(CommandStart())
async def cmd_start(message: Message):
    file_path = 'B:/file.png'
    await message.answer('Привет!', reply_markup=kb.main)
    await message.bot.send_chat_action(
        chat_id=message.chat.id,
        action=ChatAction.UPLOAD_DOCUMENT
    )
    await message.reply_photo(
        photo = types.FSInputFile(path=file_path),
        caption="Наше мобильное приложение, созданное на Unity с использованием языка C#, предлагает мини-игры, способствующие развитию когнитивных навыков, включая мнемоническую память. Основная цель игры заключается в улучшении памяти школьников, представленной в увлекательной и привлекательной форме."
    )

@router.message(F.text == 'Скачать игру')
async def download(message: Message):
    file_path = 'B:/MemDev.apk'
    await message.bot.send_chat_action(
        chat_id=message.chat.id,
        action=ChatAction.UPLOAD_DOCUMENT
    )
    await message.reply_document(
        document=types.FSInputFile(path=file_path),
        caption='Этот файл APK необходим для запуска игры. Скачайте его и установите на своё устройство, чтобы начать играть.'
    )

@router.message(F.text == 'О игре')
async def about_game(message: Message):
    await message.answer('Приложение включает в себя 4 мини-игры, каждая из которых сопровождается кратким описанием.\nДоступны три уровня сложности.\nЛучшие результаты сохраняются в статистике и могут быть просмотрены в соответствующем разделе.')

@router.message(F.text == 'О нас')
async def about_us(message: Message):
    await message.answer('MemDev (MemoryDevelopment)')
