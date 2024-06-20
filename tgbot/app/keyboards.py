from aiogram.types import ReplyKeyboardMarkup, KeyboardButton

main = ReplyKeyboardMarkup(keyboard=[[KeyboardButton(text = 'Скачать игру')],
                                     [KeyboardButton(text = 'О игре')],
                                     [KeyboardButton(text = 'О нас')]],
                            resize_keyboard = True,
                            input_field_placeholder = 'Выберите пункт меню...')
