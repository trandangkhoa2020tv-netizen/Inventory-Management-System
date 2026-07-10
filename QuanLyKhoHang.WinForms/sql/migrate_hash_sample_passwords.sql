-- ============================================================
-- Doi cac mat khau mau dang plain text/SHA-256/PBKDF2 cu sang PBKDF2.
-- Tai khoan demo sau migration:
--   admin / 123456
--   nhanvienkho / 123456
--   nhanvienbanhang / 123456
-- Chay file nay cho database cu da seed sample_data.sql truoc khi bo hash.
-- Code hien tai van chap nhan plain text de tuong thich, nen migration nay an toan va khong bat buoc.
-- ============================================================

UPDATE taikhoan
SET mat_khau = 'pbkdf2$100000$cWxraC1hZG1pbi0xMjM0NTYtc2FsdC12MQ==$D42Ak1eqSBNJflAoIDRvaAMOsz7NF5X7UQjvDwGr0xk='
WHERE ten_taikhoan = 'admin'
  AND mat_khau IN (
      'admin123',
      '123456',
      '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9',
      '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',
      'pbkdf2$100000$cWxraC1hZG1pbi1zYWx0LXYx$lOctHBqPmdhFZLUgAMvE2r5aknrFc/20Khp5yLTyr+s='
  );

UPDATE taikhoan
SET mat_khau = 'pbkdf2$100000$cWxraC1zdGFmZi1zYWx0LXYx$dS8VgTfJ0gRv1mu5WUKd36fm95MT4+wSG9lI5rlplZk='
WHERE ten_taikhoan IN ('nhanvienkho', 'nhanvienbanhang')
  AND mat_khau IN (
      '123456',
      '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'
  );
