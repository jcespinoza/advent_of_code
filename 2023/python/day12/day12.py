import sys

def part_one(lines) -> int:
  total = 0
  for line in lines:
    text, sizes = parse_condition_record(line)
    text = "."+text+"."
    arrangements = get_possible_arrangements(text, sizes)
    total += arrangements
  
  return total

def parse_condition_record(line: str) -> tuple[str,tuple[int]]:
  text, sizes = line.split(" ")
  sizes = tuple(map(int, sizes.split(",")))
  
  return text, sizes

def get_possible_arrangements(text: str, sizes: tuple[int]) -> int:
  if not sizes:
    return 0 if "#" in text else 1
  
  size = sizes[0]
  rest = sizes[1:]
  
  result = 0
  for end in range(len(text)):
    start = end - (size - 1)
    
    if segment_fits(text, start, end):
      result += get_possible_arrangements(text[end + 1 :], rest)
    
  return result

def segment_fits(text: str, start: int, end: int) -> bool:
  if start - 1 < 0 or end + 1 >= len(text):
    return False
  
  if text[start - 1] == "#" or text[end + 1] == "#":
    return False
  
  if "#" in text[:start]:
    return False
  
  for index in range(start, end + 1):
    if text[index] == ".":
      return False
    
  return True

def part_one_solution():
  lines = []
  with open("./day12/puzzle_input.txt", "r") as f:
    lines = f.read().splitlines()

  print(part_one(lines))
  
def part_one_example1():
  lines = [
    "???.### 1,1,3",
    ".??..??...?##. 1,1,3",
    "?#?#?#?#?#?#?#? 1,3,1,6",
    "????.#...#... 4,1,1",
    "????.######..#####. 1,6,5",
    "?###???????? 3,2,1",
  ]
  print(part_one(lines))
  
part_one_example1()