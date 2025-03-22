import pandas as pd

class LFSRCipher:
    def __init__(self, lfsr, message):
        self.lfsr = list(lfsr)
        self.message_bytes = message
        self.shifted_bits = []
        self.crypted = bytearray(len(message))
        self.lfsr_states = []
        self.siphre()

    def polynomial(self, lfsr):
        bit26 = int(lfsr[0])
        bit7 = int(lfsr[19])
        bit6 = int(lfsr[20])
        bit0 = int(lfsr[26])
        return bit26 ^ bit7 ^ bit6 ^ bit0

    def siphre(self):
        for i in range(55):
            key_byte = 0

            for bit in range(8):
                new_bit = self.polynomial(self.lfsr)
                key_byte |= (new_bit << bit)
                self.shifted_bits.append(self.lfsr[0])

                # Сохраняем текущее состояние регистра и XOR в таблицу
                self.lfsr_states.append(self.lfsr + [str(new_bit)])

                # Сдвиг регистра влево и добавление нового бита в конец
                self.lfsr = self.lfsr[1:] + [str(new_bit)]

            # Шифрование/дешифрование по XOR
            self.crypted[i] = self.message_bytes[i] ^ key_byte

    def print_input_bits(self):
        return ''.join(f"{byte:08b}" for byte in self.message_bytes)

    def print_output_bits(self):
        return ''.join(f"{byte:08b}" for byte in self.crypted)

    def print_shifted_bits(self):
        return ''.join(self.shifted_bits)

    def get_res(self):
        return self.crypted

    def save_to_excel(self, filename="lfsr_states.xlsx"):
        # Создаем DataFrame с состояниями регистра
        columns = [f"{i}" for i in range(27, 0, -1)] + ["XOR"]
        df = pd.DataFrame(self.lfsr_states, columns=columns)
        df.to_excel(filename, index=False)


# Пример использования
if __name__ == "__main__":
    lfsr_state = "111111111111111111111111111"  # Начальное состояние - все 1
    message = b"gggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg"
    cipher = LFSRCipher(lfsr_state, message)

    print("Input Bits:", cipher.print_input_bits())
    print("Output Bits:", cipher.print_output_bits())
    print("Shifted Bits:", cipher.print_shifted_bits())

    # Сохраняем состояние регистра в Excel
    cipher.save_to_excel()
