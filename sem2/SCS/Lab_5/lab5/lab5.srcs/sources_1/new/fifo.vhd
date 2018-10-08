library IEEE;
use IEEE.STD_LOGIC_1164.ALL;
use IEEE.NUMERIC_STD.ALL;
use ieee.std_logic_unsigned.all;

entity fifo is
     Port (clk : in  STD_LOGIC;
           reset : in  STD_LOGIC;
           rdinc: in STD_LOGIC;
           wrinc: in STD_LOGIC;
           rd: in STD_LOGIC;
           wr: STD_LOGIC;
           data_in: in STD_LOGIC_VECTOR(7 downto 0);
           data_out : out STD_LOGIC_VECTOR(7 downto 0));
end fifo;

architecture Behavioral of fifo is

    component dff is
        port (clk: in std_logic;
        d: in std_logic_vector(7 downto 0);
        en: in std_logic;
        rst: in std_logic;
        q: out std_logic_vector(7 downto 0));
    end component;
    
    component debouncer is
        Port (d_in: in STD_LOGIC;
              q_out: out STD_LOGIC;
              clk: in STD_LOGIC;
              rst: in STD_LOGIC);
    end component;

    signal wrptr: STD_LOGIC_VECTOR(2 downto 0) := "000";
    signal rdptr: STD_LOGIC_VECTOR(2 downto 0) := "000";
    signal dec: STD_LOGIC_VECTOR(7 downto 0) := "00000000";
    signal fifo7: STD_LOGIC_VECTOR(7 downto 0) := "00000000";
    signal fifo6: STD_LOGIC_VECTOR(7 downto 0) := "00000000";
    signal fifo5: STD_LOGIC_VECTOR(7 downto 0) := "00000000";
    signal fifo4: STD_LOGIC_VECTOR(7 downto 0) := "00000000";
    signal fifo3: STD_LOGIC_VECTOR(7 downto 0) := "00000000";
    signal fifo2: STD_LOGIC_VECTOR(7 downto 0) := "00000000";
    signal fifo1: STD_LOGIC_VECTOR(7 downto 0) := "00000000";
    signal fifo0: STD_LOGIC_VECTOR(7 downto 0) := "00000000";
    
    signal en0: STD_LOGIC;
    signal en1: STD_LOGIC;
    signal en2: STD_LOGIC;
    signal en3: STD_LOGIC;
    signal en4: STD_LOGIC;
    signal en5: STD_LOGIC;
    signal en6: STD_LOGIC;
    signal en7: STD_LOGIC;
    signal rdincout: STD_LOGIC;
    signal wrincout: STD_LOGIC;
    signal count : integer :=1;
    signal clk1 : std_logic :='0';
    
    signal buffer_in: STD_LOGIC_VECTOR(7 downto 0);
    
begin
    DB1: debouncer port map(rdinc,rdincout,clk,'0');
    DB3: debouncer port map(wrinc,wrincout,clk,'0');
    
    frq_divider: process(clk)
        begin
            if rising_edge(clk) then
              count <=count+1;
            if(count = 500000) then
                clk1 <= not clk1;
                count <=1;
                
            end if;
            end if;
            end process;
    
    wr_pointer: process(clk1)
    begin
    if reset = '1' then
        wrptr <= "000";
    else
        if (rising_edge(clk1) and wrincout = '1') then
            wrptr <= wrptr + 1;
        end if;
    end if;
    end process wr_pointer;
    
    rd_pointer: process(clk1)
        begin
        if reset = '1' then
            rdptr <= "000";
        else
            if (rising_edge(clk1)) then
                if rdincout = '1' then
                    rdptr <= rdptr + 1;
                    --data_out <= "11111111";
                end if;
            end if;
        end if;
        end process rd_pointer;

    decoder: process(wrptr)
    begin
        case wrptr is
            when "000" => dec <= "00000001";
            when "001" => dec <= "00000010";
            when "010" => dec <= "00000100";
            when "011" => dec <= "00001000";
            when "100" => dec <= "00010000";
            when "101" => dec <= "00100000";
            when "110" => dec <= "01000000";
            when "111" => dec <= "10000000";
        end case;
    end process decoder;
    
    en0 <= wr and dec(0);
    en1 <= wr and dec(1);
    en2 <= wr and dec(2);
    en3 <= wr and dec(3);
    en4 <= wr and dec(4);
    en5 <= wr and dec(5);
    en6 <= wr and dec(6);
    en7 <= wr and dec(7);
    
    R0: dff port map(clk => clk ,d => data_in, en => en0, rst => reset, q => fifo0);
    R1: dff port map(clk => clk ,d => data_in, en => en1, rst => reset, q => fifo1);
    R2: dff port map(clk => clk ,d => data_in, en => en2, rst => reset, q => fifo2);
    R3: dff port map(clk => clk ,d => data_in, en => en3, rst => reset, q => fifo3);
    R4: dff port map(clk => clk ,d => data_in, en => en4, rst => reset, q => fifo4);
    R5: dff port map(clk => clk ,d => data_in, en => en5, rst => reset, q => fifo5);
    R6: dff port map(clk => clk ,d => data_in, en => en6, rst => reset, q => fifo6);
    R7: dff port map(clk => clk ,d => data_in, en => en7, rst => reset, q => fifo7);
    
    MUX: process(rdptr)
    begin
        case rdptr is
            when "000" => buffer_in <= fifo0;
            when "001" => buffer_in <= fifo1;
            when "010" => buffer_in <= fifo2;
            when "011" => buffer_in <= fifo3;
            when "100" => buffer_in <= fifo4;
            when "101" => buffer_in <= fifo5;
            when "110" => buffer_in <= fifo6;
            when "111" => buffer_in <= fifo7;
            end case;
    end process MUX;
    
    TRISTATE: process(rd)
    begin
        if rd = '1' then
            data_out <= "00000" & wrptr;--buffer_in;
        else
            data_out <= "ZZZZZZZZ";
        end if;
    end process TRISTATE;

end Behavioral;