library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

entity debouncer is
    Port (d_in: in STD_LOGIC;
          q_out: out STD_LOGIC;
          clk: in STD_LOGIC;
          rst: in STD_LOGIC);
end debouncer;

architecture Behavioral of debouncer is
   
signal Q1, Q2, Q3 : std_logic;
signal 	clk1 : std_logic :='0';

begin
    
--**Insert the following after the 'begin' keyword**
    process(clk)
    begin
       if (rising_edge(clk)) then
          if (rst = '1') then
             Q1 <= '0';
             Q2 <= '0';
             Q3 <= '0';
          else
             Q1 <= d_in;
             Q2 <= Q1;
             Q3 <= Q2;
          end if;
       end if;
    end process;

    q_out <= Q1 and Q2 and  (not Q3);

end Behavioral;
