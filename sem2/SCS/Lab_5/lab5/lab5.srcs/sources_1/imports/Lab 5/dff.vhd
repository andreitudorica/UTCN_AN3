library ieee;
use ieee.std_logic_1164.all;

entity dff is
    port (clk: in std_logic;
    d: in std_logic_vector(7 downto 0);
    en: in std_logic;
    rst: in std_logic;
    q: out std_logic_vector(7 downto 0));
end dff;

architecture example of dff is
begin
    process (clk)
    begin
        if rst = '1' then
            q <= "00000000";
        else
            if (clk'event and clk = '1') then
                if en = '1' then
                    q <= d;
                end if;
            end if;
        end if;
    end process;
end example;
