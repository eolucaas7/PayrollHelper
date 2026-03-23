-- Первичный ключ для связующей таблицы
ALTER TABLE IF EXISTS salary_and_bonuses_taxation 
ADD CONSTRAINT pk_salary_bonuses_taxation 
PRIMARY KEY (salary_and_bonuses_id, taxation_id);

-- Связь: сотрудник -> должность
ALTER TABLE IF EXISTS employees
ADD CONSTRAINT fk_employees_position 
FOREIGN KEY (post_number) 
REFERENCES positions (id) 
ON UPDATE NO ACTION 
ON DELETE NO ACTION;

-- Связь: выплата -> сотрудник
ALTER TABLE IF EXISTS payments
ADD CONSTRAINT fk_payments_employee 
FOREIGN KEY (employee_id) 
REFERENCES employees (employee_id) 
ON UPDATE NO ACTION 
ON DELETE NO ACTION;

-- Связь: зарплаты-налоги -> зарплаты
ALTER TABLE IF EXISTS salary_and_bonuses_taxation
ADD CONSTRAINT fk_sbt_salary 
FOREIGN KEY (salary_and_bonuses_id) 
REFERENCES salary_and_bonuses (id) 
ON UPDATE NO ACTION 
ON DELETE NO ACTION;

-- Связь: зарплаты-налоги -> налоги
ALTER TABLE IF EXISTS salary_and_bonuses_taxation
ADD CONSTRAINT fk_sbt_taxation 
FOREIGN KEY (taxation_id) 
REFERENCES taxation (id) 
ON UPDATE NO ACTION 
ON DELETE NO ACTION;