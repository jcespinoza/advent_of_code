import pytest
from day12.day12 import part_one

@pytest.fixture
def day_12_fixture():
    # setUp()
    obj = None  # whatever you want to pass
    yield obj
    # tearDown()
    
    
def test_part_one(day_12_fixture):
  result = part_one([
    '#.#.### 1,1,3',
    '.#...#....###. 1,1,3',
    '.#.###.#.###### 1,3,1,6',
    '####.#...#... 4,1,1',
    '#....######..#####. 1,6,5',
    '.###.##....# 3,2,1',
  ])
  assert result == 212