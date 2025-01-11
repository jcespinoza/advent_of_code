pub struct GiftBox {
  pub length: u32,
  pub width: u32,
  pub height: u32,
}

impl GiftBox {
  pub fn new(length: u32, width: u32, height: u32) -> Self {
    Self {
      length,
      width,
      height,
    }
  }

  pub fn surface_area(&self) -> u32 {
    2 * self.length * self.width + 2 * self.width * self.height + 2 * self.height * self.length
  }

  pub fn smallest_area(&self) -> u32 {
    let mut sides = [self.length, self.width, self.height];
    sides.sort();
    sides[0] * sides[1]
  }
}

#[cfg(test)]
mod tests {
  use super::*;

  #[test]
  fn test_surface_area() {
    let box1 = GiftBox::new(2, 3, 4);
    assert_eq!(box1.surface_area(), 52);
  }

  #[test]
  fn test_smallest_area() {
    let box1 = GiftBox::new(2, 3, 4);
    assert_eq!(box1.smallest_area(), 6);
  }

  #[test]
  fn test_smallest_area_2() {
    let box1 = GiftBox::new(1, 1, 10);
    assert_eq!(box1.smallest_area(), 1);
  }

  #[test]
  fn test_smallest_area_3() {
    let box1 = GiftBox::new(1, 10, 1);
    assert_eq!(box1.smallest_area(), 1);
  }
}
